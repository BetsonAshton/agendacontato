using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Política de autenticação(Cookie)

//permitindo a aplicação trabalhar com Cookies
builder.Services.Configure<CookiePolicyOptions>
(options => { options.MinimumSameSitePolicy = SameSiteMode.None; });

builder.Services.AddAuthentication
(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

//ativando a configuração do cookie
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
