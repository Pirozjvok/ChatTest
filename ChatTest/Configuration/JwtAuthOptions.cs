namespace ChatTest.Configuration
{
    public class JwtAuthOptions
    {
        #pragma warning disable CS8618

        public string AccessSigningKey { get; set; }

        public string RefreshSigningKey { get; set; }

        public int AccessTokenLifetime { get; set; } = 5;

        public int RefreshTokenLifetime { get; set; } = 7;
    }
}
