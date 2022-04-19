using Microsoft.AspNetCore.Components;

namespace TheWorkBook.Pages
{
    public partial class Jobdetails : ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        //[Parameter]
        public string Text { get; set; }

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
