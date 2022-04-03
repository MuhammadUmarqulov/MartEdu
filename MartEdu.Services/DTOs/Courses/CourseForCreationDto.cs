using MartEdu.Domain.Enums.Courses;
using Microsoft.AspNetCore.Http;

namespace MartEdu.Service.DTOs.Courses
{
    public class CourseForCreationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Hashtag Teg { get; set; }
        public Level Level { get; set; }
        public Section Section { get; set; }
        public IFormFile Image { get; set; }
    }
}
