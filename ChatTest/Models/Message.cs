namespace ChatTest.Models
{
    public class Message
    {
        #pragma warning disable CS8618

        public int Id { get; set; }

        public User? User { get; set; }

        public int UserId { get; set; }

        public Chat? Chat { get; set; }

        public int ChatId { get; set; }

        public DateTimeOffset DateTime { get; set; }

        public string Text { get; set; }
    }
}
