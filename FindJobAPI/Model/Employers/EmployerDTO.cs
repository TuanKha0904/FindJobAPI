using FindJobAPI.Model.Domain;
using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.Employers
{
    public class EmployerDTO
    {
        public int employer_id { get; set; }
        public string? employer_name { get; set; }
        public string? employer_about { get; set; }
        public string? employer_address { get; set; }
        public string? contact_phone { get; set; }
        public string? employer_image { get; set; }
        public string? employer_website { get; set; }
    }

    public class EmployerNoId
    {
        public string? employer_name { get; set; }
        public string? employer_about { get; set; }
        public string? employer_address { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid PhoneNumber!")]
        public string? contact_phone { get; set; }
        public string? employer_image { get; set; }
        public string? employer_website { get; set; }
    }
}
