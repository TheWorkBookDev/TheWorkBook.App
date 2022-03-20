using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using TheWorkBook.Data;

namespace TheWorkBook;

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

        List<string> loopbackAddresses = new()
        {
            "127.0.0.1",
            "192.168.1.5"
        };

        builder.Services.AddTransient<OidcClient>(sp =>
            new OidcClient(new OidcClientOptions
            {
                Authority = "http://192.168.1.5:5001",
                ClientId = "native",
                RedirectUri = "theworkbook://authorize",
                Scope = "openid profile api offline_access",
                Browser = sp.GetRequiredService<WebAuthenticatorBrowser>(),

                // When locally debugging
                Policy = new Policy() { Discovery = new IdentityModel.Client.DiscoveryPolicy() { RequireHttps = false, LoopbackAddresses = loopbackAddresses } },
            })
        );

        // When using https://auth.theworkbook.ie.
        //builder.Services.AddTransient<OidcClient>(sp =>
        //   new OidcClient(new OidcClientOptions
        //   {
        //       Authority = "https://auth.theworkbook.ie",
        //       ClientId = "native",
        //       RedirectUri = "theworkbook://connect/authorize",
        //       Scope = "openid profile api offline_access",
        //       Browser = sp.GetRequiredService<WebAuthenticatorBrowser>()
        //   })
        //);

        return builder.Build();
    }

    private static void ConfigureConfiguration(MauiAppBuilder builder)
    {
        var assembly = typeof(App).GetTypeInfo().Assembly;
        builder.Configuration.AddJsonFile(new EmbeddedFileProvider(assembly), "appsettings.json", optional: false, false);
    }
}
