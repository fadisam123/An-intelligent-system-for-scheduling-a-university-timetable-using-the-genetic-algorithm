using Microsoft.Extensions.DependencyInjection;
using Timetable.Application.Services.DataIO.Course;
using Timetable.Application.Services.DataIO.DayTime;
using Timetable.Application.Services.DataIO.Room;
using Timetable.Application.Services.DataIO.Survey;
using Timetable.Application.Services.DataIO.Teacher;

namespace Timetable.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IDayTimeService, DayTimeService>();
            services.AddScoped<ILectureService, LectureService>();
            services.AddScoped<ISurveyService, SurveyService>();

            return services;
        }

    }
}