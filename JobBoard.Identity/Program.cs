using JobBoard.Identity;
using JobBoard.Identity.Data;
using JobBoard.Identity.Interfaces;
using JobBoard.Identity.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AuthDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["DbConnection"]);
});

builder.Services.AddDbContext<IJobDbContext, JobDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["JobDbConnection"]);
});

builder.Services.AddIdentity<AppUser, IdentityRole>(opts =>
{
    opts.Password.RequiredLength = 8;
    opts.Password.RequireDigit = true;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = true;
    opts.Password.RequireUppercase = true;
    opts.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AuthDbContext>()
  .AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
                .AddAspNetIdentity<AppUser>()
                .AddInMemoryApiResources(Configuration.ApiResources)
                .AddInMemoryIdentityResources(Configuration.IdentityResources)
                .AddInMemoryApiScopes(Configuration.ApiScopes)
                .AddInMemoryClients(Configuration.Clients)
                .AddDeveloperSigningCredential();

builder.Services.ConfigureApplicationCookie(opts =>
{
    opts.CookieManager = new ChunkingCookieManager();
    opts.Cookie.HttpOnly = true;
    opts.Cookie.SameSite = SameSiteMode.Strict;
    opts.Cookie.Name = "JobBoard.Identity.Cookie";
    opts.LoginPath = "/Auth/Login";
    opts.LogoutPath = "/Auth/Logout";
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseIdentityServer();
app.EnsureDbCreated();
app.EnsureSystemAdministratorExist();
app.MapControllers();
app.Run();
