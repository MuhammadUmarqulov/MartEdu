using System.ComponentModel.DataAnnotations;


namespace MartEdu.Services.DTOs.Users
{
    public class UserForCreationDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
