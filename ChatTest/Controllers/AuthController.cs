using Microsoft.EntityFrameworkCore;
using ChatTest.Database;
using ChatTest.DTO;
using ChatTest.Models;
using ChatTest.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ChatTest.Extensions;
using ChatTest.Services.TokenHelper;

namespace ChatTest.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private DefaultContext _dbContext;

        private IPasswordHasher<User> _passwordHasher;

        private ITokenHelper _tokenHelper;

        public AuthController(DefaultContext dbContext, IPasswordHasher<User> passwordHasher, ITokenHelper tokenHelper)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _tokenHelper = tokenHelper;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(TokenPair))]
        [ProducesResponseType(409, Type=typeof(ProblemDetails))]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            User user = new User()
            {
                Name = registerDTO.Name,
                Email = registerDTO.Email.ToLower(),
            };

            if (_dbContext.Users.Any(x => x.Email == user.Email))
            {
                return Problem("A user with this email already exists", statusCode: 409);
            }

            user.PasswordHash = _passwordHasher.HashPassword(user, registerDTO.Password);
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return await CreateToken(user.Id);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(TokenPair))]
        [ProducesResponseType(401, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            User? user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == loginDTO.Email);

            if (user == null || !_passwordHasher.Verify(user, user.PasswordHash, loginDTO.Password))
            {
                return Problem("Email or password is incorrect", statusCode: 401);
            }

            return await CreateToken(user.Id);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(TokenPair))]
        [ProducesResponseType(401, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Refresh([FromQuery]string? refreshToken)
        {
            if (refreshToken == null)
                refreshToken = Request.Cookies[".AspNetCore.Application.Id"];

            if (refreshToken == null)
                return Problem("Missing refresh token", statusCode: 400);

            Dictionary<string, string>? claims = _tokenHelper.ParseToken(refreshToken, TokenType.RefreshToken);

            if (claims == null)
            {
                return Problem("A refresh token is invalid", statusCode: 401);
            }

            Guid refreshTokenId = Guid.Parse(claims["jti"]);

            TokenEntity? token = await _dbContext.Tokens.FirstOrDefaultAsync(x => x.Id == refreshTokenId);

            if (token == null)
            {
                return Problem("The token has been deleted", statusCode: 401);
            }

            _dbContext.Remove(token);

            return await CreateToken(token.UserId);
        }

        [HttpGet]
        public async Task<IActionResult> Logout([FromQuery] string? refreshToken)
        {
            if (refreshToken == null)
                refreshToken = Request.Cookies[".AspNetCore.Application.Id"];

            if (refreshToken == null)
                return Problem("Missing refresh token", statusCode: 400);


            Response.Cookies.Delete(".AspNetCore.Application.Id", new CookieOptions()
            {
                SameSite = SameSiteMode.Strict
            });
            Response.Cookies.Delete(".AspNetCore.Application.Id2", new CookieOptions()
            {
                SameSite = SameSiteMode.Strict
            });

            Dictionary<string, string>? claims = _tokenHelper.ParseToken(refreshToken, TokenType.RefreshToken);

            if (claims == null)
            {
                return Problem("A refresh token is invalid", statusCode: 401);
            }

            Guid refreshTokenId = Guid.Parse(claims["jti"]);

            TokenEntity? token = await _dbContext.Tokens.FirstOrDefaultAsync(x => x.Id == refreshTokenId);

            if (token == null)
            {
                return Problem("The token has been deleted", statusCode: 401);
            }

            _dbContext.Remove(token);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        private async Task<IActionResult> CreateToken(int userId)
        {
            TokenPair tokenInfo = _tokenHelper.CreateTokens(userId);

            TokenEntity token = new TokenEntity()
            {
                Id = tokenInfo.RefreshTokenId,
                Token = tokenInfo.RefreshToken,
                UserId = userId,
                Expires = tokenInfo.RefreshTokenExpires.ToUnixTimeSeconds()
            };

            _dbContext.Tokens.Add(token);

            await _dbContext.SaveChangesAsync();

            Response.Cookies.Append(".AspNetCore.Application.Id", tokenInfo.RefreshToken, new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                Path = "/Auth/",
                Expires = tokenInfo.RefreshTokenExpires,
                SameSite = SameSiteMode.Strict
            });

            Response.Cookies.Append(".AspNetCore.Application.Id2", tokenInfo.AccessToken, new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                Path = "/",
                Expires = tokenInfo.AccessTokenExpires,
                SameSite = SameSiteMode.Strict
            });

            return Ok(tokenInfo);
        }
    }
}
