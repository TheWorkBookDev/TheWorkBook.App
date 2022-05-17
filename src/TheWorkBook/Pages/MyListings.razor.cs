using Microsoft.AspNetCore.Components;
using TheWorkBook.Data;
using TheWorkBook.Dto;
using TheWorkBook.Models;
using TheWorkBook.ViewModels;

namespace TheWorkBook.Pages;
public partial class MyListings : ComponentBase
{
    [Inject]
    private ListingService _listingService { get; set; }

    private MyJobListingsViewModel _myJobListingsViewModel { get; set; } = new MyJobListingsViewModel();

    protected async override Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _myJobListingsViewModel.Listings = await _listingService.GetMyListings();
    }

    private bool HasSearchResults
    {
        get
        {
            if (_myJobListingsViewModel.Listings != null
                && _myJobListingsViewModel.Listings.Count > 0)
            {
                return true;
            }

            return false;
        }
    }
}
