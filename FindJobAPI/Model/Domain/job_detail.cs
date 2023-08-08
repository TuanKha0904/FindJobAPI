namespace FindJobAPI.Model.Domain
{
    public class job_detail
    {
        //navigation properties: one job-detail just has one job
        public int job_id { get; set; }
        public job? job {  get; set; }
        public string? job_title { get; set; }
        public string? job_description { get; set; }

        public string? location { get; set; }
        public string? requirement { get; set; }
        public float minimum_salary { get; set; }
        public float maximum_salary { get; set; }
        public bool status { get; set; }

        //navigation properties: one job-detail just has one industry
        public industry? industry_id { get; set; }
    
    }
}
