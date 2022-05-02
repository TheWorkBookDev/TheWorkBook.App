using Microsoft.AspNetCore.Components;
using TheWorkBook.Data;
using TheWorkBook.Dto;
using TheWorkBook.ViewModels;

namespace TheWorkBook.Pages;

public partial class PostJobBegin : ComponentBase
{
    [Parameter]
    public int CategoryId { get; set; }

    [Inject]
    private UserService UserService { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }


    protected async override Task OnInitializedAsync()
    {
        // We call GetMyInfo to ensure the user is authenticated
        // and initiate the login flow if they are not.
        _ = await UserService.GetMyInfo();

        NavigationManager.NavigateTo("/postjob");

        await base.OnInitializedAsync();
    }
}

