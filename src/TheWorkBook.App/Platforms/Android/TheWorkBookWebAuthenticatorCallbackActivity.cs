using Android.App;
using Android.Content;
using Android.Content.PM;

namespace TheWorkBook.App;

[Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
[IntentFilter(new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
    DataScheme = CALLBACK_SCHEME)]
public class BookStoreWebAuthenticatorCallbackActivity : Microsoft.Maui.Essentials.WebAuthenticatorCallbackActivity
{
    const string CALLBACK_SCHEME = "theworkbook";
}
