using TheWorkBook.Dto;

namespace TheWorkBook.ViewModels;

public class PostJobViewModel
{
    public List<CategoryDto> Categories { get; set; }

    public EditListingDto EditListingDto { get; set; } = new EditListingDto();
    public List<LocationDto> Locations { get; set; }
}