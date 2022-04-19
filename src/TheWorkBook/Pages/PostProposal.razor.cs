using Microsoft.AspNetCore.Components;

namespace TheWorkBook.Pages
{
    public partial class PostProposal : ComponentBase
    {
        [Inject]
        private NavigationManager _navigationManager { get; set; }

        private void Submit()
        {
            _navigationManager.NavigateTo("joblisting");
        }
    }
}
