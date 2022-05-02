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
        private CategoryService CategoryService { get; set; }

        [Inject]
        private LocationService LocationService { get; set; }

        [Inject]
        private ListingService ListingService { get; set; }

        [Inject]
        private UserService UserService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected PostJobViewModel viewModel = new PostJobViewModel();

        protected async override Task OnInitializedAsync()
        {
            // We call GetMyInfo to ensure the user is authenticated
            // and initiate the login flow if they are not.
            _ = await UserService.GetMyInfo();

            viewModel.Categories = await CategoryService.GetCategoriesAsync();
            viewModel.Locations = await LocationService.GetCounties();

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

            await ListingService.CreateListing(newListingDto);

            NavigationManager.NavigateTo("/");
        }

        private void GoToSubmitProposal()
        {
            NavigationManager.NavigateTo("postproposal");
        }

        private void UpdateJobDetails()
        {

        }
    }
}
