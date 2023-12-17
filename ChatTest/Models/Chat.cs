using System.ComponentModel.DataAnnotations.Schema;

namespace ChatTest.Models
{
    public class Chat
    {
        #pragma warning disable CS8618

        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public List<User> Users { get; set; }
    }
}
