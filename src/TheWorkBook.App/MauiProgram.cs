using System.Reflection;
using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using TheWorkBook.App.Data;

namespace TheWorkBook.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .RegisterBlazorMauiWebView()
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        ConfigureConfiguration(builder);

        builder.Services.AddBlazorWebView();
        builder.Services.AddSingleton<WeatherForecastService>();

        builder.Services.AddTransient<WebAuthenticatorBrowser>();

        builder.Services.AddTransient<MainPage>();

        // When locally debugging
        //List<string> loopbackAddresses = new List<string>();
        //loopbackAddresses.Add("127.0.0.1");
        //loopbackAddresses.Add("192.168.1.5");

        builder.Services.AddTransient<OidcClient>(sp =>
            new OidcClient(new OidcClientOptions
            {
                //Authority = "http://192.168.1.5:5001",
                Authority = "https://auth.theworkbook.ie",
                ClientId = "native",
                RedirectUri = "theworkbook://authorized",
                Scope = "openid profile api offline_access",
                Browser = sp.GetRequiredService<WebAuthenticatorBrowser>(),

                // When locally debugging
                //Policy = new Policy() { Discovery = new IdentityModel.Client.DiscoveryPolicy() { RequireHttps = false, LoopbackAddresses = loopbackAddresses } },
            })
        );

        return builder.Build();
    }

    private static void ConfigureConfiguration(MauiAppBuilder builder)
    {
        var assembly = typeof(App).GetTypeInfo().Assembly;
        builder.Configuration.AddJsonFile(new EmbeddedFileProvider(assembly), "appsettings.json", optional: false, false);
    }
}
