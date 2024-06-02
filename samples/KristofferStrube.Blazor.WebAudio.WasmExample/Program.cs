using KristofferStrube.Blazor.MediaCaptureStreams;
using KristofferStrube.Blazor.SVGEditor.Extensions;
using KristofferStrube.Blazor.WebAudio.WasmExample;
using KristofferStrube.Blazor.WebIDL;
using KristofferStrube.Blazor.Window;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMediaDevicesService();
builder.Services.AddSVGEditor();
builder.Services.AddWindowService();

WebAssemblyHost app = builder.Build();

await app.Services.SetupErrorHandlingJSInterop();

await app.RunAsync();