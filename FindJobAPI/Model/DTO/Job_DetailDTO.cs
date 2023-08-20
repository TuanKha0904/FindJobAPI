using FindJobAPI.Model.Domain;

namespace FindJobAPI.Model.DTO
{
    public class Job_DetailNoId
    {
        public string? job_title { get; set; }
        public string? job_description { get; set; }
        public string? location { get; set; }
        public string? requirement { get; set; }
        public float minimum_salary { get; set; }
        public float maximum_salary { get; set; }
        public bool status { get; set; }
        public int industry_id { get; set; }
    }

    public class Update_JobDetail
    {
        public string? job_title { get; set; }
        public string? job_description { get; set; }
        public string? location { get; set; }
        public string? requirement { get; set; }
        public float minimum_salary { get; set; }
        public float maximum_salary { get; set; }
        public int industry_id { get; set; }
    }

    public class Update_Status_Job
    {
        public bool status { get; set; }
    }
}
