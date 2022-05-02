using Microsoft.AspNetCore.Components;
using TheWorkBook.Data;
using TheWorkBook.Dto;
using TheWorkBook.Models;
using TheWorkBook.ViewModels;

namespace TheWorkBook.Pages;
public partial class JobListing : ComponentBase
{
    [Parameter]
    public int CategoryId { get; set; }

    [Inject]
    private CategoryService _categoryService { get; set; }

    [Inject]
    private LocationService _locationService { get; set; }

    [Inject]
    private SearchService _searchService { get; set; }

    private JobListingViewModel _jobListingViewModel { get; set; } = new JobListingViewModel();

    private bool _isFilterVisible;

    IEnumerable<int> multipleValues = new int[] { 1, 2 };


    private void ToogleFilterDrawer()
    {
        _isFilterVisible = !_isFilterVisible;
    }

    private async Task UpdateSearch()
    {
        _isFilterVisible = false;
        await PerformSearch();
    }

    private async Task PerformSearch()
    {
        _jobListingViewModel.SearchResponse = new SearchResponse();

        List<int> categories = new List<int>() { CategoryId };
        _jobListingViewModel.SearchResponse = await _searchService.SearchListingsAsync(categories, null);
    }

    protected async override Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _jobListingViewModel.SearchList.Add(new Job() { Id = 1, Title = ".NET Developer" });
        _jobListingViewModel.SearchList.Add(new Job() { Id = 2, Title = "Xamarin Developer" });
        _jobListingViewModel.SearchList.Add(new Job() { Id = 3, Title = "Blazor Developer" });
        _jobListingViewModel.SearchList.Add(new Job() { Id = 4, Title = "MAUI Developer" });

        await PerformSearch();
        _jobListingViewModel.SelectedCategory = await _categoryService.GetCategoryAsync(CategoryId);
        _jobListingViewModel.LocationList = await _locationService.GetCounties();
    }

    private bool HasSearchResults
    {
        get
        {
            if (_jobListingViewModel.SearchResponse != null
                && _jobListingViewModel.SearchResponse.Listings != null
                && _jobListingViewModel.SearchResponse.Listings.Count > 0)
            {
                return true;
            }

            return false;
        }
    }

    private void OnSearchChange(object value, string name)
    {

    }

    void OnLocationChange()
    {
        //var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;
    }
}
