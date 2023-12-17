using System.ComponentModel.DataAnnotations;

namespace ChatTest.DTO
{
    public class RegisterDTO
    {
        #pragma warning disable CS8618

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
