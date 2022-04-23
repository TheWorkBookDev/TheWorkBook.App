using TheWorkBook.Dto;

namespace TheWorkBook.ViewModels
{
    public class PostJobViewModel
    {
        public List<CategoryDto> Categories { get; set; }

        public int CategoryId { get; set; }

        public List<LocationDto> Locations { get; set; }

        public int LocationId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        
        public decimal Budget { get; set; }
    }
}
