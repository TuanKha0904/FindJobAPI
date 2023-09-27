using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.DTO
{
    public class LocationDTO
    {
        public int location_id { get; set; }
        public string? location { get; set; }
    }

    public class LocationNoId
    {
        [Required(ErrorMessage = "Please enter location!")]
        public string? location { get; set; }
    }
}