using System.ComponentModel.DataAnnotations;

namespace ChatTest.DTO
{
    public class LoginDTO
    {
        #pragma warning disable CS8618

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
