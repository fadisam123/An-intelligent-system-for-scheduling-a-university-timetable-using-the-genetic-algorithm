using FluentValidation;
using FluentValidation.AspNetCore;
using Timetable.RazorWeb.Validators;
using Timetable.RazorWeb.ViewModels;

namespace Timetable.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddValidatorServices(this IServiceCollection services)
        {
            services.AddFluentValidationClientsideAdapters();
            services.AddScoped<IValidator<TeacherViewModel>, TeacherViewModelValidator>();
            return services;
        }

    }
}