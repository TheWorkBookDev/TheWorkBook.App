using Microsoft.AspNetCore.Components;

namespace TheWorkBook.Pages
{
    public partial class PostJob : ComponentBase
    {
        [Parameter]
        public int CategoryId { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }
        
        private void GoToSubmitProposal()
        {
            _navigationManager.NavigateTo("postproposal");
        }

        private void UpdateJobDetails()
        {

        }

    }
}
