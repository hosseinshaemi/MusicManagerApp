using Spotify.Data;
using Spotify.Utils;
using Spotify.Utils.Contracts;
using Spotify.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Spotify.Data.Repositories.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SpotifyContext>(option =>
{
    option.UseSqlite(builder.Configuration.GetConnectionString("SpotifyApp"));
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
