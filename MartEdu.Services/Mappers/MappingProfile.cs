using AutoMapper;
using MartEdu.Domain.Entities.Courses;
using MartEdu.Domain.Entities.Users;
using MartEdu.Service.DTOs.Courses;
using MartEdu.Service.DTOs.Users;

namespace MartEdu.Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseForCreationDto>().ReverseMap();
            CreateMap<User, UserForCreationDto>().ReverseMap();
            CreateMap<User, UserForLoginDto>().ReverseMap();
        }
    }
}
