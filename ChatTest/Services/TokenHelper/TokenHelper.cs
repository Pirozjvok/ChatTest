using ChatTest.Configuration;
using ChatTest.Models;
using ChatTest.Results;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatTest.Services.TokenHelper
{
    public class TokenHelper : ITokenHelper
    {
        private JwtSecurityTokenHandler _tokenHandler;

        private SymmetricSecurityKey _accessSecurityKey;

        private SymmetricSecurityKey _refreshSecurityKey;

        private JwtAuthOptions _jwtAuthOptions;

        public TokenHelper(IConfiguration configuration)
        {
            _tokenHandler = new JwtSecurityTokenHandler();
            _jwtAuthOptions = new JwtAuthOptions();

            configuration.GetSection("JwtAuth").Bind(_jwtAuthOptions);

            _accessSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtAuthOptions.AccessSigningKey));
            _refreshSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtAuthOptions.RefreshSigningKey));
        }

        public Dictionary<string, string>? ParseToken(string token, TokenType tokenType)
        {
            SecurityKey key = tokenType == TokenType.AccessToken ? _accessSecurityKey : _refreshSecurityKey;

            TokenValidationParameters parameters = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                IssuerSigningKey = key,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateActor = false,
            };

            try
            {
                ClaimsPrincipal claimsPrincipal = _tokenHandler.ValidateToken(token, parameters, out _);
                return claimsPrincipal.Claims.ToDictionary(claim => claim.Type, claim => claim.Value);
            }
            catch
            {
                return null;
            }
        }

        public TokenPair CreateTokens(int userId)
        {
            (string accessToken, DateTimeOffset atExp) = CreateAccessToken(userId);
            (string refreshToken, DateTimeOffset rtExp, Guid id) = CreateRefreshToken(userId);

            return new TokenPair(refreshToken, accessToken)
            {
                RefreshTokenExpires = rtExp,
                RefreshTokenId = id,
                AccessTokenExpires = atExp,             
            };
        }

        private (string, DateTimeOffset, Guid) CreateRefreshToken(int userId)
        {
            Guid guid = Guid.NewGuid();
            var claims = new List<Claim> 
            { 
                new Claim("sub", userId.ToString()),
                new Claim("jti", guid.ToString()) 
            };
            var refreshTokenExp = DateTimeOffset.UtcNow.AddDays(_jwtAuthOptions.RefreshTokenLifetime);
            string token = CreateToken(claims, _refreshSecurityKey, refreshTokenExp);
            return (token, refreshTokenExp, guid);
        }

        private (string, DateTimeOffset) CreateAccessToken(int userId)
        {
            var claims = new List<Claim> { new Claim("sub", userId.ToString()) };
            var accessTokenExp = DateTimeOffset.UtcNow.AddMinutes(_jwtAuthOptions.AccessTokenLifetime);
            string token = CreateToken(claims, _accessSecurityKey, accessTokenExp);
            return (token, accessTokenExp);
        }

        private string CreateToken(List<Claim> claims, SymmetricSecurityKey key, DateTimeOffset exp)
        {
            var jwt = new JwtSecurityToken(
                claims: claims,
                expires: exp.UtcDateTime,
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            return _tokenHandler.WriteToken(jwt);
        }
    }
}
