using CarRentingSystem.Configuration;
using CarRentingSystem.Constants;
using CarRentingSystem.Infrastructure.Data;
using CarRentingSystem.Infrastructure.Data.Models;
using CarRentingSystem.Infrastructure.Extensions;
using CarRentingSystem.ModelBinders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:RequireConfirmedAccount");
    options.SignIn.RequireConfirmedEmail = builder.Configuration.GetValue<bool>("Identity:RequireConfirmedEmail");
    options.SignIn.RequireConfirmedPhoneNumber = builder.Configuration.GetValue<bool>("Identity:RequireConfirmedPhoneNumber");
    options.Password.RequiredLength = builder.Configuration.GetValue<int>("Identity:RequiredLength");
    options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:RequireNonAlphanumeric");
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
    {
        options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
    });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Home/Index";
});

builder.Host.ConfigureAppConfiguration((context, builder) =>
{
    builder.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: false)
    .AddEnvironmentVariables();
});
builder.Services.AddApplicationServices();

var emailConfigurationSection = builder.Configuration.GetSection(EmailConfigurationConstants.EmailConfiguration);
builder.Services.Configure<EmailConfiguration>(emailConfigurationSection);

builder.Host.UseSerilog((hostContext, logger) =>
{
    logger.ReadFrom.Configuration(hostContext.Configuration);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSerilogRequestLogging();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "Car Details",
        pattern: "/Cars/Details/{id}/{information}",
        defaults: new { Controller = "Car", Action = "Details" });

    endpoints.MapDefaultControllerRoute();
    endpoints.MapRazorPages();
});

app.SeedAdmin();

app.Run();
