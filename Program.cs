// https://www.youtube.com/watch?v=sZnu-TyaGNk
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlite("DataSource=app.db"));

builder.Services.AddIdentityCore<MyUser>()
.AddEntityFrameworkStores<AppDbContext>()
.AddApiEndpoints();

var app = builder.Build();

app.MapIdentityApi<MyUser>();
app.MapGet("/", (ClaimsPrincipal user) => $"Hello {user.Identity?.Name}!");
app.Run();

class MyUser : IdentityUser { };

class AppDbContext : IdentityDbContext<MyUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
