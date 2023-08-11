using FluentValidation;
using FluentValidation.AspNetCore;
using Timetable.RazorWeb.Validators;
using Timetable.RazorWeb.ViewModels.InputModels;

namespace Timetable.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddValidatorServices(this IServiceCollection services)
        {
            services.AddFluentValidationClientsideAdapters();
            services.AddScoped<IValidator<TeacherInputModel>, TeacherInputModelValidator>();
            services.AddScoped<IValidator<CourseInputModel>, CourseInputModelValidator>();
            return services;
        }

    }
}