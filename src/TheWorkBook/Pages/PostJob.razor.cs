using Microsoft.AspNetCore.Components;
using TheWorkBook.Data;
using TheWorkBook.Dto;
using TheWorkBook.ViewModels;

namespace TheWorkBook.Pages;

public partial class PostJob : ComponentBase
{
    [Parameter]
    public int CategoryId { get; set; }

    [Parameter]
    public int ListingId { get; set; }

    [Inject]
    private CategoryService CategoryService { get; set; }

    [Inject]
    private LocationService LocationService { get; set; }

    [Inject]
    private ListingService ListingService { get; set; }

    [Inject]
    private UserService UserService { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    private UserDto userDto;

    protected PostJobViewModel viewModel = new PostJobViewModel();

    protected async override Task OnInitializedAsync()
    {
        // We call GetMyInfo to ensure the user is authenticated
        // and initiate the login flow if they are not.
        userDto = await UserService.GetMyInfo();

        // If a ListingId is passed in, then we are editing an existing listing
        if (ListingId != 0)
        {
            await PopulateViewModelForExistingListing();
        }

        viewModel.Categories = await CategoryService.GetCategoriesAsync();
        viewModel.Locations = await LocationService.GetCounties();

        await base.OnInitializedAsync();
    }

    private async Task PopulateViewModelForExistingListing()
    {
        if (userDto == null)
            return;

        viewModel.EditListingDto = await ListingService.GetEditListing(ListingId);

        // We should check that the user owns the listing
        if (viewModel.EditListingDto != null
            && viewModel.EditListingDto.UserId != userDto.UserId)
        {          
            // Redirect out of this screen, this isn't the user's listing
            // to edit.
            NavigationManager.NavigateTo("/");
        }
    }

    public async Task PostJobClicked()
    {
        if (ListingId == 0)
            await ListingService.CreateListing(viewModel.EditListingDto);
        else await ListingService.UpdateListing(viewModel.EditListingDto);

        string navigateTo = "/postjobsuccess";
        if (CategoryId > 0)
            navigateTo += "/" + CategoryId;
        
        NavigationManager.NavigateTo(navigateTo);
    }

    private string CallToActionText
    {
        get
        {
            if (ListingId > 0)
                return "Update Job";
            else return "Post Job";
        }
    }
}
