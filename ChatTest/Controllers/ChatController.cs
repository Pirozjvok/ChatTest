using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ChatTest.DTO;
using ChatTest.Models;
using ChatTest.Database;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using ChatTest.Services.EventBus;

namespace ChatTest.Controllers
{
    [Route("[controller].[action]")]
    [Authorize]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private DefaultContext _dbContext;

        public ChatController(DefaultContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody] CreateChatDTO createChat)
        {
            int userId = GetUserId();
            User user = await _dbContext.Users.FirstAsync(x => x.Id == userId);

            Chat chat = new Chat()
            {
                Name = createChat.Name,
                UserId = user.Id,
                Users = new List<User>() { user }             
            };

            _dbContext.Chats.Add(chat);
            await _dbContext.SaveChangesAsync();

            ChatDTO chatDTO = new ChatDTO()
            {
                Id = chat.Id,
                Name = chat.Name
            };

            return Ok(chatDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToChat([FromForm]int chatId, [FromForm] int addUserId)
        {
            int userId = GetUserId();

            User? user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == addUserId);

            if (user == null)
                return Problem("User with this id is not exists", statusCode: 404);

            Chat? chat = await _dbContext.Chats.FirstOrDefaultAsync(x => x.Id == chatId);

            if (chat == null)
                return Problem("Chat with this id is not exists", statusCode: 404);

            if (chat.UserId != userId)
                return Problem("You don't have permission to modify this chat.", statusCode: 403);

            ChatUser chatUser = new ChatUser()
            {
                ChatId = chatId,
                UserId = addUserId
            };

            bool contains = await _dbContext.ChatUsers.ContainsAsync(chatUser);

            if (contains)
                return Problem("A user with this id is already in the chat");
            
            _dbContext.ChatUsers.Add(chatUser);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToChatByName([FromForm] int chatId, [FromForm] string name)
        {
            int userId = GetUserId();

            User? user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Name == name);

            if (user == null)
                return Problem("User with this name is not exists", statusCode: 404);

            Chat? chat = await _dbContext.Chats.FirstOrDefaultAsync(x => x.Id == chatId);

            if (chat == null)
                return Problem("Chat with this id is not exists", statusCode: 404);

            /*
            if (chat.UserId != userId)
                return Problem("You don't have permission to modify this chat.", statusCode: 403);
            */

            ChatUser chatUser = new ChatUser()
            {
                ChatId = chatId,
                UserId = user.Id
            };

            bool contains = await _dbContext.ChatUsers.ContainsAsync(chatUser);

            if (contains)
                return Problem("A user with this id is already in the chat");

            _dbContext.ChatUsers.Add(chatUser);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetChatUsers([FromQuery] int chatId)
        {
            int userId = GetUserId();

            Chat? chat = await _dbContext.Chats
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Id == chatId);

            if (chat == null)
                return Problem("A chat with this id is not exists");

            bool contains = chat.Users.Any(x => x.Id == userId);

            if (!contains)
                return Problem("You don't have permission to read this chat.", statusCode: 403);     

            List<UserDTO> users = chat.Users
                .Select(x => new UserDTO() { Id = x.Id, Name = x.Name })
                .ToList();

            return Ok(users);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserChats()
        {
            int userId = GetUserId();

            User user = await _dbContext.Users
                .Include(x => x.Chats)
                .FirstAsync(x => x.Id == userId);

            List<ChatDTO> chats = user.Chats.Select(x => new ChatDTO() { Id = x.Id, Name = x.Name }).ToList();

            return Ok(chats);
        }

        [HttpGet]
        public async Task<IActionResult> GetChatMessages([FromQuery] int chatId)
        {
            int userId = GetUserId();

            Chat? chat = await _dbContext.Chats
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Id == chatId);

            if (chat == null)
                return Problem("A chat with this id is not exists");

            bool contains = chat.Users.Any(x => x.Id == userId);

            if (!contains)
                return Problem("You don't have permission to read this chat.", statusCode: 403);

            List<Message> messages = await _dbContext.Messages
                .Where(x => x.ChatId == chatId)
                .Include(x => x.User)
                .ToListAsync();

            List<MessageDTO> messageDTOs = messages
                .Select(x => new MessageDTO()
                {
                    DateTime = x.DateTime,
                    Id = x.Id,
                    Text = x.Text,
                    User = new UserDTO() { Id = x.User!.Id, Name = x.User.Name }

                }).ToList();

            return Ok(messageDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> SendChatMessage([FromForm] int chatId, [FromForm] string text, [FromServices]IEventBus<MessageDTO> eventBus)
        {
            int userId = GetUserId();

            Chat? chat = await _dbContext.Chats
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Id == chatId);

            if (chat == null)
                return Problem("A chat with this id is not exists");

            bool contains = chat.Users.Any(x => x.Id == userId);

            if (!contains)
                return Problem("You don't have permission to read this chat.", statusCode: 403);

            User user = await _dbContext.Users.FirstAsync(x => x.Id == userId);

            Message message = new Message()
            {
                ChatId = chatId,
                Text = text,
                UserId = userId,
                DateTime = DateTimeOffset.UtcNow
            };

            _dbContext.Messages.Add(message);

            await _dbContext.SaveChangesAsync();

            MessageDTO messageDTO = new MessageDTO()
            {
                DateTime = message.DateTime,
                Id = message.Id,
                Text = message.Text,
                ChatId = message.ChatId,
                User = new UserDTO() { Id = user.Id, Name = user.Name }
            };

            eventBus.Publish(messageDTO);

            return Ok(messageDTO);
        }

        private int GetUserId()
        {
            string claim = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            return int.Parse(claim);
        }
    }
}
