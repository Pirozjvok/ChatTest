namespace ChatTest.Results
{
    public class TokenPair
    {
        public string RefreshToken { get; set; }

        internal DateTimeOffset RefreshTokenExpires { get; set; }

        internal Guid RefreshTokenId { get; set; }

        public string AccessToken { get; set; }

        internal DateTimeOffset AccessTokenExpires { get; set; }

        public TokenPair(string refreshToken, string accessToken)
        {
            RefreshToken = refreshToken;
            AccessToken = accessToken;
        }
    }
}
