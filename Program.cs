using Microsoft.AspNetCore.Builder;
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


app.UseHttpsRedirection();
app.UseSession();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
