
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

using OwinCore.data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddDbContext<AppAuthContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("default")));


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{

    option.Cookie.HttpOnly = true;
    option.Cookie.SecurePolicy = builder.Environment.IsDevelopment()
    ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
    option.Cookie.SameSite = SameSiteMode.Lax;
    option.Cookie.Name = "SimpleTalk.AuthCookieAspNetCore";
    option.LoginPath = "/Home/Login";
    option.LogoutPath = "/Home/Logout";



    

});


builder.Services.AddMvc(option => option.Filters.Add(new AuthorizeFilter())

);



builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
    options.HttpOnly = HttpOnlyPolicy.None;
    options.Secure = builder.Environment.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
});








var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();



    app.UseCookiePolicy();
    app.UseAuthentication();

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
