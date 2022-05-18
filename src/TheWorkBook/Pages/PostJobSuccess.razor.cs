using Microsoft.AspNetCore.Components;
using TheWorkBook.Data;
using TheWorkBook.Dto;
using TheWorkBook.ViewModels;

namespace TheWorkBook.Pages;

public partial class PostJobSuccess : ComponentBase
{
    [Parameter]
    public int CategoryId { get; set; }

    [Parameter]
    public int ListingId { get; set; }

    [Inject]
    private UserService UserService { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    protected async override Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }
}

