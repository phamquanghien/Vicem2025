using DemoMVC.Services.Implementations;
using DemoMVC.Services.Interfaces;

namespace DemoMVC.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Đăng ký các Services cụ thể
            services.AddScoped<ICourseService, CourseService>();
            return services;
        }
    }
}