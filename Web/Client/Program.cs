using KratosBlazorApp.Client;
using KratosBlazorApp.Client.Handlers;
using KratosBlazorApp.Client.Managers;
using KratosBlazorApp.Shared.Providers;
using KratosBlazorApp.Shared.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Ory.Kratos.Client.Client;
using System.Net;
using System.Net.Http.Headers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env}.json", true, true);

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IOryKratosIdentityManager, OryKratosIdentityManager>();
builder.Services.AddScoped<UserStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<UserStateProvider>());
builder.Services
    .AddTransient<SetCookieHandler>();
builder.Services.AddHttpClient("Set-Cookie", client => client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"))).AddHttpMessageHandler<SetCookieHandler>();
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Set-Cookie"));


await builder.Build().RunAsync();
