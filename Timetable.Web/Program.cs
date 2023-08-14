using Timetable.Domain.Enums;
using Timetable.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Register my own Dependency for each project
await builder.Services.AddInfrastructureServicesAsync(builder.Configuration);

#region Authentication and autorization
builder.Services.AddAuthentication().AddCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Teacher", policy =>
    {
        policy.RequireRole(RoleEnum.DepartmentHead.ToString(), RoleEnum.Professor.ToString(), RoleEnum.LapTeacher.ToString());
    });

    options.AddPolicy("Admin", policy =>
    {
        policy.RequireRole(RoleEnum.Admin.ToString());
    });
});
#endregion

builder.Services.AddApplicationServices();

builder.Services.AddValidatorServices();

// Add services to the container.
builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
{
    // The default page when app start
    options.Conventions.AddPageRoute("/Admin/schedule", "");
    //options.Conventions.AuthorizeFolder(folderPath: "/Admin", policy: "Admin");
    //options.Conventions.AuthorizeFolder(folderPath: "/Teacher", policy: "Teacher");
    //options.Conventions.AllowAnonymousToFolder("/Account");
});


builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();
