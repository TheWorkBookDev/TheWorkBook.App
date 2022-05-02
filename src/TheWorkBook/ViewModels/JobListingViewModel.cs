using TheWorkBook.Dto;
using TheWorkBook.Models;

namespace TheWorkBook.ViewModels;

public class JobListingViewModel
{
    public CategoryDto SelectedCategory { get; set; } = new CategoryDto();

    public List<Job> SearchList { get; set; } = new List<Job>();

    public List<LocationDto> LocationList { get; set; } = new List<LocationDto>();
    public SearchResponse SearchResponse { get; set; } = new SearchResponse();
}