using TheWorkBook.Dto;
using TheWorkBook.Models;

namespace TheWorkBook.ViewModels;

public class MyJobListingsViewModel
{
    public List<ListingDto> Listings { get; set; } = new List<ListingDto>();
}