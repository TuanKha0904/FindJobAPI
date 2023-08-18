namespace FindJobAPI.Model.DTO
{
    public class SeekerDTO
    {
        public int seeker_id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? address { get; set; }
        public DateTime birthday { get; set; }
        public string? phone_number { get; set; }
        public string? seeker_image { get; set; }
        public string? academic_level { get; set; }
        public string? skills { get; set; }
        public string? url_cv { get; set; }
        public string? website_seeker { get; set; }
    }

    public class SeekerNoId
    {
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? address { get; set; }
        public DateTime birthday { get; set; }
        public string? phone_number { get; set; }
        public string? seeker_image { get; set; }
        public string? academic_level { get; set; }
        public string? skills { get; set; }
        public string? url_cv { get; set; }
        public string? website_seeker { get; set; }
    }
}
