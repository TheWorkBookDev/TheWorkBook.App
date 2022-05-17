using Microsoft.AspNetCore.Components;
using TheWorkBook.Data;
using TheWorkBook.Dto;

namespace TheWorkBook.Pages;

public partial class JobDetails : ComponentBase
{
    [Parameter]
    public int Id { get; set; }

    //[Parameter]
    public string Text { get; set; }

    public ListingDto ListingDto { get; set; } = new ListingDto();

    public UserDto UserDto { get; set; } = new UserDto();

    [Inject]
    private ListingService _listingService { get; set; }

    [Inject]
    private UserService _userService { get; set; }

    [Inject]
    private NavigationManager _navigationManager { get; set; }

    protected async override Task OnInitializedAsync()
    {
        // Fetching the listing from the API
        ListingDto = await _listingService.GetListing(Id);

        // If the user is logged in, then lets retrieve their information
        UserDto = await _userService.GetMyInfoAuthCheck();

        await base.OnInitializedAsync();
    }

    private bool IsUsersListing()
    {
        if (UserDto == null)
            return false;

        return UserDto.UserId == ListingDto.UserId;
    }
}
