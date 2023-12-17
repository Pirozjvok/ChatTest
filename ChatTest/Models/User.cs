namespace ChatTest.Models
{
    public class User
    {
        #pragma warning disable CS8618

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public List<Chat> Chats { get; set; }
    }
}
