using MartEdu.Data.IRepositories;
using MartEdu.Data.Repositories;
using MartEdu.Services.Interfaces;
using MartEdu.Services.Services;
using Microsoft.Extensions.DependencyInjection;


namespace MartEdu.Api.Extensions
{
    internal static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
