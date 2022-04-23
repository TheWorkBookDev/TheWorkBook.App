using Microsoft.AspNetCore.Components;
using TheWorkBook.Data;
using TheWorkBook.Dto;
using TheWorkBook.ViewModels;

namespace TheWorkBook.Pages
{
    public partial class PostJob : ComponentBase
    {
        [Parameter]
        public int CategoryId { get; set; }

        [Inject]
        private CategoryService _categoryService { get; set; }

        [Inject]
        private LocationService _locationService { get; set; }

        [Inject]
        private ListingService _listingService { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        protected PostJobViewModel viewModel = new PostJobViewModel();

        protected async override Task OnInitializedAsync()
        {
            viewModel.Categories = await _categoryService.GetCategoriesAsync();
            viewModel.Locations = await _locationService.GetCounties();

            await base.OnInitializedAsync();
        }
        
        public async Task PostJobClicked()
        {
            var newListingDto = new NewListingDto()
            {
                CategoryId = viewModel.CategoryId,
                MainDescription = viewModel.Description,
                LocationId = viewModel.LocationId,
                Title = viewModel.Title
            };

            await _listingService.CreateListing(newListingDto);

            _navigationManager.NavigateTo("/");
        }

        private void GoToSubmitProposal()
        {
            _navigationManager.NavigateTo("postproposal");
        }

        private void UpdateJobDetails()
        {

        }
    }
}
