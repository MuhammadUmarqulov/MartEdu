using MartEdu.Data.IRepositories;
using MartEdu.Data.Repositories;
using MartEdu.Domain.Entities.Authors;
using MartEdu.Domain.Entities.Courses;
using MartEdu.Domain.Entities.Users;
using MartEdu.Service.DTOs.Authors;
using MartEdu.Service.DTOs.Courses;
using MartEdu.Service.DTOs.Users;
using MartEdu.Service.Interfaces;
using MartEdu.Service.Services;
using Microsoft.Extensions.DependencyInjection;


namespace MartEdu.Api.Extensions
{
    internal static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IGenericRepository<User>, GenericRepository<User>>();
            services.AddTransient<IGenericService<User, UserForCreationDto>, GenericService<User, UserForCreationDto>>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IGenericRepository<Author>, GenericRepository<Author>>();
            services.AddTransient<IGenericService<Author, AuthorForCreationDto>, GenericService<Author, AuthorForCreationDto>>();
            services.AddTransient<IAuthorService, AuthorService>();
            
            services.AddTransient<IGenericRepository<Course>, GenericRepository<Course>>();
            services.AddTransient<IGenericService<Course, CourseForCreationDto>, GenericService<Course, CourseForCreationDto>>(); 
            services.AddTransient<ICourseService, CourseService>();

        }
    }
}
