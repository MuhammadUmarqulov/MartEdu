using System.ComponentModel.DataAnnotations;

namespace MartEdu.Services.DTOs.Users
{
    public class UserForLoginDto
    {
        [Required]
        public string EmailOrUsername { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
