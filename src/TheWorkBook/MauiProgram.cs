using IdentityModel.OidcClient;
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
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        ConfigureConfiguration(builder);

        builder.Services.AddSingleton<AccessTokenHttpMessageHandler>();
        builder.Services.AddTransient<HttpClient>(sp =>
            new HttpClient(sp.GetRequiredService<AccessTokenHttpMessageHandler>())
            {
                BaseAddress = new Uri("https://api.theworkbook.ie")
            });

        builder.Services.AddMauiBlazorWebView();
        
        builder.Services.AddSingleton<CategoryService>();
        builder.Services.AddSingleton<ListingService>();
        builder.Services.AddSingleton<LocationService>();
        builder.Services.AddSingleton<SearchService>();
        builder.Services.AddSingleton<UserService>();

        builder.Services.AddTransient<WebAuthenticatorBrowser>();
        builder.Services.AddTransient<MainPage>();

        builder.Services.AddTransient<GoBack>();

        List<string> loopbackAddresses = new()
        {
            "127.0.0.1",
            "192.168.1.5"
        };

        // When debugging locally.
        //builder.Services.AddTransient<OidcClient>(sp =>
        //    new OidcClient(new OidcClientOptions
        //    {
        //        Authority = "http://192.168.1.5:5001",
        //        ClientId = "native",
        //        RedirectUri = "theworkbook://authorize",
        //        Scope = "openid profile api offline_access",
        //        Browser = sp.GetRequiredService<WebAuthenticatorBrowser>(),

        //        // When locally debugging
        //        Policy = new Policy() { Discovery = new IdentityModel.Client.DiscoveryPolicy() { RequireHttps = false, LoopbackAddresses = loopbackAddresses } },
        //    })
        //);

		// When using https://auth.theworkbook.ie.
		builder.Services.AddTransient<OidcClient>(sp =>
			new OidcClient(new OidcClientOptions
			{
				Authority = "https://auth.theworkbook.ie",
				ClientId = "native",
				RedirectUri = "theworkbook://authorize",
                PostLogoutRedirectUri = "theworkbook://logout",
                Scope = "openid profile api offline_access",
				Browser = sp.GetRequiredService<WebAuthenticatorBrowser>(),

				// When locally debugging
				Policy = new Policy() { Discovery = new IdentityModel.Client.DiscoveryPolicy() { RequireHttps = true, LoopbackAddresses = loopbackAddresses } },
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
