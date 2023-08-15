using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Timetable.Domain.Entities;

namespace Timetable.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<User, Role, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<TakingSurveyAllowedPeriod> TakingSurveyAllowedPeriods { get; set; }
        public DbSet<TeacherPreferenceDayTime> TeacherPreferenceDayTimes { get; set; }
        public DbSet<Year> Years { get; set; }
        public DbSet<Day> Days { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("TimeTable");

            modelBuilder.Entity<User>().ToTable(name: "User", "Security");

            modelBuilder.Entity<Role>().ToTable(name: "Role", "Security");

            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles", "Security")
                .HasKey(key => new { key.UserId, key.RoleId });

            modelBuilder.Entity<IdentityUserClaim<Guid>>(entity =>
            {
                entity.ToTable("UserClaims", "Security");
            });

            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins", "Security")
                .HasKey(key => new { key.ProviderKey, key.LoginProvider });

            modelBuilder.Entity<IdentityRoleClaim<Guid>>(entity =>
            {
                entity.ToTable("RoleClaims", "Security");
            });

            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens", "Security")
                .HasKey(key => new { key.UserId, key.LoginProvider, key.Name });

            modelBuilder.Entity<Day>().Property(d => d.DayNo).ValueGeneratedNever();
            modelBuilder.Entity<Year>().Property(y => y.YearNo).ValueGeneratedNever();
            modelBuilder.Entity<Group>().Property(g => g.GroupNo).ValueGeneratedNever();
            modelBuilder.Entity<Semester>().Property(s => s.SemesterNo).ValueGeneratedNever();

            // Set cascading delete behavior to Restrict for all relationships
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {

            builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");

            builder.Properties<TimeOnly>()
                .HaveConversion<TimeOnlyConverter>()
                .HaveColumnType("time");

            base.ConfigureConventions(builder);

        }

        public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
        {
            public DateOnlyConverter()
                : base(dateOnly =>
                       dateOnly.ToDateTime(TimeOnly.MinValue),
                       dateTime => DateOnly.FromDateTime(dateTime))
            { }
        }

        public class TimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
        {
            public TimeOnlyConverter()
                : base(timeOnly => timeOnly.ToTimeSpan(),
                       timeSpan => TimeOnly.FromTimeSpan(timeSpan))
            { }
        }
    }
}
