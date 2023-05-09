using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using UX_UI_WEB_APP.Models;
using UX_UI_WEB_APP.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();
builder.Services.AddSession(opts => { opts.Cookie.HttpOnly = true; opts.Cookie.IsEssential = true; });
builder.Services.Configure<MongoDBSettings>(
builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDBServices>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(500);
    options.Cookie.Name = "SessionId";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//  .AddCookie(options =>
//  {
//      options.Cookie.HttpOnly = true;
//      options.Cookie.SameSite = SameSiteMode.Lax;
//      options.Cookie.Name = "SimpleTalk.AuthCookieAspNetCore";
//      options.LoginPath = "/Home/LoginPage";
//      //options.LogoutPath = "/Home/HomePage";
//  });
//builder.Services.Configure<CookiePolicyOptions>(options =>
//{
//    options.MinimumSameSitePolicy = SameSiteMode.Strict;
//    options.HttpOnly = HttpOnlyPolicy.None;
//    options.Secure = CookieSecurePolicy.None & CookieSecurePolicy.Always;
//});
//builder.Services.AddMvc(options => options.Filters.Add(new AuthorizeFilter()));


var app = builder.Build();
//nuild
app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//var cookiePolicyOptions = new CookiePolicyOptions
//{
//    MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Lax
//};


app.UseHttpsRedirection();
app.UseSession();
app.UseStaticFiles();
app.UseRouting();
//app.UseCookiePolicy();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
