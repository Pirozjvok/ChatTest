namespace ChatTest.Models
{
    public class TokenEntity
    {
        #pragma warning disable CS8618

        public Guid Id { get; set; }

        public string Token { get; set; }

        public User? User { get; set; }

        public int UserId { get; set; }

        public long Expires { get; set; }
    }
}
