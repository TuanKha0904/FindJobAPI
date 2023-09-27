using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.DTO
{
    public class CV
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone_Number { get; set; }
        public string? Birthday { get; set; }
        public string? Address { get; set; }
        public string? Experience { get; set; }
        public string? Skills { get; set; }
        public string? Education { get; set; }
        public string? Major { get; set; }
    }

    public class SeekerNoId
    {
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? address { get; set; }
        public DateTime birthday { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid PhoneNumber!")]
        public string? phone_number { get; set; }
        public string? seeker_image { get; set; }
        public string? academic_level { get; set; }
        public string? skills { get; set; }
        public string? url_cv { get; set; }
        public string? website_seeker { get; set; }
    }
}
