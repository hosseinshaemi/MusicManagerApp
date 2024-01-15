using Spotify.Data;
using Spotify.Utils;
using Spotify.Policies;
using Spotify.Utils.Contracts;
using Spotify.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Spotify.Data.Repositories.Contracts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SpotifyContext>(option =>
{
    option.UseSqlite(builder.Configuration.GetConnectionString("SpotifyApp"));
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IMusicRepository, MusicRepository>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<IViewRenderService, RenderViewToString>();
builder.Services.AddSingleton<IAuthorizationHandler, AdminRequirementHandler>();
builder.Services.AddSingleton<IAws3Services, Aws3Services>();
builder.Services.AddAntiforgery(opt => opt.Cookie.Name = "forgery");

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.LoginPath = "/Account/Login";
    option.LogoutPath = "/Account/Logout";
    option.ExpireTimeSpan = TimeSpan.FromDays(7);
    option.Cookie.Name = "authentication";
    option.AccessDeniedPath = "/Account/Login";
    option.Events = new CookieAuthenticationEvents
    {
        OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization(option => option.AddPolicy("AdminPolicy", policy => policy.Requirements.Add(new AdminRequirement())));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{   
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/Home/Error";
        await next();
    }
});
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
