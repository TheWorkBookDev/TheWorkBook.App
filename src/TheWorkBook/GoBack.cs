using Microsoft.JSInterop;

namespace TheWorkBook;

public class GoBack
{
    private static IJSRuntime JSRuntime { get; set; }

    public GoBack(IJSRuntime jSRuntime)
    {
        GoBack.JSRuntime = jSRuntime;
    }

    public static async Task GoBackInTime()
    {
        if (GoBack.JSRuntime != null)
        {
            await GoBack.JSRuntime.InvokeVoidAsync("goBack");
        }
    }
}

