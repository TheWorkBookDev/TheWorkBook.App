using TheWorkBook.Dto;
using TheWorkBook.Models;

namespace TheWorkBook.ViewModels;

public class JobListingViewModel
{
    public List<LocationDto> LocationList { get; set; } = new List<LocationDto>();
    public List<Job> SearchList { get; set; } = new List<Job>();
    public SearchResponse SearchResponse { get; set; } = new SearchResponse();
    public CategoryDto SelectedCategory { get; set; } = new CategoryDto();
}