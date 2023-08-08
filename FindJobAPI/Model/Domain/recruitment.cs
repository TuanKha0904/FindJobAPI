namespace FindJobAPI.Model.Domain
{
    public class recruitment
    {
        //navigation properties: one seeker has many recruitment
        public int seeker_id { get; set; }
        public seeker? seeker { get; set; }

        //navigation properties: one job has many recruitment
        public int job_id { get; set; }
        public job? job { get; set; }
        public string? seeker_desire { get; set; }
        public DateTime registation_date { get; set; }
    }
}
