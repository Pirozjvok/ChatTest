using ChatTest.Results;

namespace ChatTest.Services.TokenHelper
{
    public interface ITokenHelper
    {
        TokenPair CreateTokens(int userId);

        Dictionary<string, string>? ParseToken(string token, TokenType tokenType);
    }
}
