using Microsoft.Extensions.DependencyInjection;
using Timetable.Application.Services.DataIO.Teacher;

namespace Timetable.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITeacherService, TeacherService>();

            return services;
        }

    }
}