using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.Domain
{
    public class employer
    {
        //navigation properties: one role has many acount
        [Key]
        public account? employer_id { get; set; }
        public string? employer_name { get; set; }
        public string? employer_about { get; set;}
        public string? employer_address { get; set; }
        public string? contact_phone { get; set; }
        public string? employer_image { get; set; }

        public string? employer_website { get; set; }

        //navigation properties: one employer has many job
        public List<job>? jobs { get; set; }
    }
}
