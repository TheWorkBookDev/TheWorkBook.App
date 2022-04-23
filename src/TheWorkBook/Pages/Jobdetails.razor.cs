using Microsoft.AspNetCore.Components;
using TheWorkBook.Data;
using TheWorkBook.Dto;

namespace TheWorkBook.Pages
{
    public partial class JobDetails : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        //[Parameter]
        public string Text { get; set; }

        public ListingDto ListingDto { get; set; } = new ListingDto();

        [Inject]
        private ListingService _listingService { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            // Fetching the listing from the API
            ListingDto = await _listingService.GetListing(Id);
            
            await base.OnInitializedAsync();            
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
