using TheWorkBook.Dto;

namespace TheWorkBook.ViewModels;

public class PostJobViewModel
{
    public List<CategoryDto> Categories { get; set; }

    public List<LocationDto> Locations { get; set; }

    public EditListingDto EditListingDto { get; set; } = new EditListingDto();
}