using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.Domain
{
    public class job
    {
        [Key]
        public int job_id { get; set; }

        //navigation properties: one job just one employer
        public employer? employer_id { get; set; }

        //navigation properties: one job just one type
        public type? type_id { get; set; }
        public DateTime posted_date { get; set; }
        public DateTime deadline { get; set; }

        //navigation properties: one job has one job detail
        public List<job_detail>? job_detail { get; set; }
    }
}
