using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.DTO
{
    public class RecruitmentDTO
    {
        public int seeker_id { get; set; }
        public int job_id { get; set; }
        public string? seeker_desire { get; set; }
        public DateTime registration_date { get; set; }
    }

    public class SeekerRecruitment
    {
        public int job_id { get; set; }
        public string? seeker_desire { get; set; }
        public DateTime registration_date { get; set; }
    }

    public class RecruitmentJob
    {
        public int seeker_id { get; set; }
        public string? seeker_desire { get; set; }
        public DateTime registration_date { get; set; }
    }

    public class CreateRecruitment
    {
        [Required (ErrorMessage = "Please enter Who are you?")]
        public int seeker_id { get; set; }

        [Required (ErrorMessage = "Please enter job then you want to recruitment")]
        public int job_id { get; set; }

        [Required(ErrorMessage = "Let's desire about you for employer")]
        public string? seeker_desire { get; set; }
    }

    public class UpdateRecruitment
    {
        [Required (ErrorMessage = "Let's desire about you for employer")]
        public string? seeker_desire { get; set; }
    }
}
