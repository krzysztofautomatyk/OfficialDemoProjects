using Blazor_WebAssemblyApp_ConnectFourGame;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Registers a singleton instance of GameState to the Dependency injection container
builder.Services.AddSingleton<Blazor_WebAssemblyApp_ConnectFourGame.Shared.GameState>(); 

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
