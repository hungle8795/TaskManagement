using Client;
using Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7126/api/") });
builder.Services.AddScoped<TaskService>();
builder.Services.AddLogging();
builder.Services.AddBlazorBootstrap();



await builder.Build().RunAsync();
