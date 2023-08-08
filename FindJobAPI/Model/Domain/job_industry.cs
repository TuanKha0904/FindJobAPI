namespace FindJobAPI.Model.Domain
{
    public class job_industry
    {
        //navigation properties: one job-industry has one industry
        public int industry_id { get; set; }
        public industry? industry { get; set; }
        public string? job { get; set; }
    }
}
