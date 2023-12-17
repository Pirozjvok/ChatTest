namespace ChatTest.DTO
{
    public class MessageDTO
    {
        #pragma warning disable CS8618

        public int Id { get; set; }

        public int ChatId { get; set; }

        public UserDTO User { get; set; }

        public string Text { get; set; }

        public DateTimeOffset DateTime { get; set; }
    }
}
