
using KratosBlazorApp.Domain.Contexts;
using KratosBlazorApp.Domain.Entities;
using KratosBlazorApp.Server.Middleware.Identity;
using KratosBlazorApp.Server.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<KratosBlazorAppContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton(new KratosService(builder.Configuration["IdentityBaseAddress"]));
builder.Services.AddAuthentication("Kratos").AddScheme<KratosAuthenticationOptions, KratosAuthenticationHandler>("Kratos", null);
builder.Services.AddCors(options =>
{
    options.AddPolicy("Kratos", builder =>
     builder.WithOrigins("https://localhost:44323", "https://localhost:4433/").AllowAnyMethod().AllowAnyHeader());
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("Kratos");
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
