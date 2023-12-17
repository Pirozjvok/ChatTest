using ChatTest.Database;
using ChatTest.DTO;
using ChatTest.Models;
using ChatTest.Services.EventBus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Channels;

namespace ChatTest.Controllers
{
    public class WebSocketController : ControllerBase, IDisposable
    {
        private IDisposable _disposable;

        private Channel<MessageDTO> _channel;

        private int _userId = -1;

        private DefaultContext _dbContext;

        public WebSocketController(IEventBus<MessageDTO> eventBus, DefaultContext dbContext)
        {
            _disposable = eventBus.Subscribe(OnMessageReceived);
            UnboundedChannelOptions options = new UnboundedChannelOptions()
            {
                SingleReader = true,
                SingleWriter = true,
            };
            _channel = Channel.CreateUnbounded<MessageDTO>(options);
            _dbContext = dbContext;
        }

        private void OnMessageReceived(MessageDTO message)
        {
            _channel.Writer.TryWrite(message);
        }

        void IDisposable.Dispose() => _disposable.Dispose();

        [Route("/ws")]
        [Authorize]
        [HttpGet]
        public async Task Get(CancellationToken cancellationToken)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                string claim = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                _userId = int.Parse(claim);
                await HandleWs(webSocket, cancellationToken);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        private async Task HandleWs(WebSocket webSocket, CancellationToken cancellationToken)
        {
            Task reciever = Recieve(webSocket, cancellationToken);
            while (!cancellationToken.IsCancellationRequested)
            {
                MessageDTO message = await _channel.Reader.ReadAsync(cancellationToken);

                ChatUser chatUser = new ChatUser()
                {
                    ChatId = message.ChatId,
                    UserId = _userId
                };

                bool contains = await _dbContext.ChatUsers.ContainsAsync(chatUser);

                if (!contains)
                    continue;


                using (MemoryStream ms = new MemoryStream())
                {
                    var options = new JsonSerializerOptions
                    {
                        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                        WriteIndented = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };
                    JsonSerializer.Serialize(ms, message, options);
                    byte[] bytes = ms.ToArray();
                    await webSocket.SendAsync(bytes, WebSocketMessageType.Text, true, cancellationToken);
                }
            }
        }

        private async Task Recieve(WebSocket webSocket, CancellationToken cancellationToken)
        {
            var buffer = new byte[1024 * 4];
            var receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), cancellationToken);

            while (!receiveResult.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(
                    new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                    receiveResult.MessageType,
                    receiveResult.EndOfMessage,
                    cancellationToken);

                receiveResult = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer), cancellationToken);
            }

            await webSocket.CloseAsync(
                receiveResult.CloseStatus.Value,
                receiveResult.CloseStatusDescription,
                cancellationToken);
        }
    }
}
