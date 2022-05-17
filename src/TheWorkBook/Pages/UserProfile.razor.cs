using Microsoft.AspNetCore.Components;
using TheWorkBook.Data;
using TheWorkBook.Dto;

namespace TheWorkBook.Pages;

public partial class UserProfile : ComponentBase
{
    [Inject]
    private UserService _userService { get; set; }

    protected UserDto MyInfo;

    private async Task Submit()
    {
        // Save the user's details (post to the API!)
        await _userService.UpdateMyInfo(MyInfo);
        await GoBack.GoBackInTime();
    }

    protected async override Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        MyInfo = await _userService.GetMyInfo();
    }
}
