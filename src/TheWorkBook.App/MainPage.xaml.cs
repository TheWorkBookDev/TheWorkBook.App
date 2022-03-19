using IdentityModel.OidcClient;
namespace TheWorkBook.App;

public partial class MainPage : ContentPage
{
    protected OidcClient OidcClient { get; }

    public MainPage(
        OidcClient oidcClient)
    {
        InitializeComponent();
        OidcClient = oidcClient;
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var loginResult = await OidcClient.LoginAsync(new LoginRequest());
        if (loginResult.IsError)
        {
            await DisplayAlert("Error", loginResult.Error, "Close");
            return;
        }

        await SecureStorage.SetAsync(OidcConsts.AccessTokenKeyName, loginResult.AccessToken);
        await SecureStorage.SetAsync(OidcConsts.RefreshTokenKeyName, loginResult.RefreshToken);
    }
}
