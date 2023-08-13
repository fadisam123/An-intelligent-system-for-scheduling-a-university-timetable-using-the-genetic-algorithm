using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Timetable.Infrastructure.Persistence;
using Timetable.Infrastructure.Persistence.Repository;
using Timetable.Infrastructure.Persistence.UnitOfWork;
using Timetable.Application.Persistence.Repository;
using Timetable.Application.Persistence.UnitOfWork;

namespace Timetable.Infrastructure
{
    public static class DependencyInjection
    {
        public static async Task<IServiceCollection> AddInfrastructureServicesAsync(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddDbContextPool<AppDbContext>(option =>
                    option.UseSqlServer(configuration.GetConnectionString("DevelopmentConnection"),
                    SqlOption => SqlOption.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
                          .UseLazyLoadingProxies())
                .AddIdentity<User, Role>(options =>
                    {
                        options.Password.RequiredLength = 6;
                        options.Password.RequiredUniqueChars = 1;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireDigit = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireLowercase = false;

                        options.User.RequireUniqueEmail = false;
                        options.SignIn.RequireConfirmedEmail = false;

                        options.Lockout.AllowedForNewUsers = false;
                        //options.Lockout.MaxFailedAccessAttempts = 3;
                        //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                    }
                ).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IYearRepository, YearRepository>();
            services.AddScoped<ISemesterRepository, SemesterRepository>();
            services.AddScoped<IDayRepository, DayRepository>();
            services.AddScoped<ITimeRepository, TimeRepository>();
            services.AddScoped<ILectureRepository, LectureRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            await SeedDataInDbAsync(services.BuildServiceProvider());
            return services;
        }

        private static async Task SeedDataInDbAsync(IServiceProvider services)
        {
            using (IServiceScope serviceScope = services.CreateScope())
            {
                IServiceProvider serviceProvider = serviceScope.ServiceProvider;
                var DbContext = serviceProvider.GetRequiredService<AppDbContext>();
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
                await DataSeeder.SeedDataAsync(DbContext, userManager, roleManager);
            }
        }
    }
}