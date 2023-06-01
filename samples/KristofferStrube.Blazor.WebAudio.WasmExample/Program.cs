using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using KristofferStrube.Blazor.WebAudio.WasmExample;
using KristofferStrube.Blazor.MediaCaptureStreams;
using KristofferStrube.Blazor.WebIDL;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMediaDevicesService();

var app = builder.Build();

await app.Services.SetupErrorHandlingJSInterop();

await app.RunAsync();