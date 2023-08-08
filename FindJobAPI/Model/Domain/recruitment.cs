namespace FindJobAPI.Model.Domain
{
    public class recruitment
    {
        //navigation properties: one seeker has many recruitment
        public seeker? seeker_id { get; set; }

        //navigation properties: one job has many recruitment
        public job? job_id { get; set; }
        public string? seeker_desire { get; set; }
        public DateTime registation_date { get; set; }
    }
}
