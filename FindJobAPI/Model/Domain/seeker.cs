using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.Domain
{
    public class seeker
    {
        //navigation properties: one role has many acount
        [Key]
        public int seeker_id { get; set; }
        public string? email{ get; set; }
        public account? account { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set;}
        public string? address { get; set; }
        public DateTime birthday { get; set; }
        public string? phone_number { get; set; }
        public string? seeker_image { get; set; }
        public string? academic_level { get; set; }
        public string? skills { get; set; }
        public string? url_cv { get; set; }
        public string? website_seeker { get; set; }

        //navigation properties: one seeker has many recruitment
        public List<recruitment>? recruitments { get; set; }

    }
}
