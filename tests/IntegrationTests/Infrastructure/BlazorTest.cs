using BlazorServer;
using KristofferStrube.Blazor.MediaCaptureStreams;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Playwright;

namespace KristofferStrube.Blazor.WebAudio.IntegrationTests.Infrastructure;

[TestFixture]
public class BlazorTest<TEvaluationContext> where TEvaluationContext : EvaluationContext, IEvaluationContext<TEvaluationContext>
{
    private IHost? _host;

    protected Uri RootUri;
    protected virtual string[] Args => ["--use-fake-device-for-media-stream", "--use-fake-ui-for-media-stream"];


    protected TEvaluationContext EvaluationContext { get; set; } = default!;
    protected TEvaluationContext EvaluationContextCreator(IServiceProvider sp)
    {
        EvaluationContext = TEvaluationContext.Create(sp);
        EvaluationContext.AfterRenderAsync = AfterRenderAsync;
        return EvaluationContext;
    }
    protected Func<Task<object?>>? AfterRenderAsync { get; set; }

    protected IPlaywright PlaywrightInstance { get; set; }
    protected IBrowser Browser { get; set; }
    protected IBrowserContext Context { get; set; }
    protected IPage Page { get; set; }

    [OneTimeSetUp]
    public async Task Setup()
    {
        PlaywrightInstance = await Playwright.CreateAsync();
        Browser = await PlaywrightInstance.Chromium.LaunchAsync(new()
        {
            Args = Args,
        });
        // Create a new incognito browser context
        Context = await Browser.NewContextAsync();
        // Create a new page inside context.
        Page = await Context.NewPageAsync();

        _host = BlazorServer.Program.BuildWebHost([],
            serviceBuilder =>
            {
                _ = serviceBuilder
                    .AddScoped(typeof(EvaluationContext), EvaluationContextCreator)
                    .AddMediaDevicesService();
            }
        );

        await _host.StartAsync();

        RootUri = new(_host.Services.GetRequiredService<IServer>().Features
            .GetRequiredFeature<IServerAddressesFeature>()
            .Addresses.Single());
    }

    [TearDown]
    public void CleanupBetweenTestRuns()
    {
        EvaluationContext.Exception = null;
        EvaluationContext.Result = null;
    }

    [OneTimeTearDown]
    public async Task TearDown()
    {
        if (Page is not null)
        {
            await Page.CloseAsync();
        }
        if (Context is not null)
        {
            await Context.CloseAsync();
        }
        if (Browser is not null)
        {
            await Browser.CloseAsync();
        }
        if (PlaywrightInstance is not null)
        {
            PlaywrightInstance.Dispose();
        }
        if (_host is not null)
        {
            await _host.StopAsync();
            _host.Dispose();
        }
    }

    protected async Task OnAfterRerenderAsync()
    {
        _ = await Page.GotoAsync(RootUri.AbsoluteUri);
        await Assertions.Expect(Page.GetByTestId("result")).ToHaveTextAsync($"done");
    }
}