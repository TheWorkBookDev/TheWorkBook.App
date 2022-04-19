using Microsoft.AspNetCore.Components;
using TheWorkBook.Data;
using TheWorkBook.Dto;

namespace TheWorkBook.Pages
{
    public partial class UserProfile : ComponentBase
    {
        [Inject]
        private UserService _userService { get; set; }        
        
        [Inject]
        private NavigationManager _navigationManager { get; set; }

        protected UserDto MyInfo;

        private void Submit()
        {
            _navigationManager.NavigateTo("index");
        }

        protected async override Task OnInitializedAsync()
        {
            MyInfo = await _userService.GetMyInfo();
            await base.OnInitializedAsync();
        }
    }
}
