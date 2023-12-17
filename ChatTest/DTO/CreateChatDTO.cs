using System.ComponentModel.DataAnnotations;

namespace ChatTest.DTO
{
    public class CreateChatDTO
    {
        #pragma warning disable CS8618

        [Required]
        [StringLength(30)]
        public string Name { get; set; }
    }
}
