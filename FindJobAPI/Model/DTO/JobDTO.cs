using FindJobAPI.Model.Domain;
using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.DTO
{
    public class JobDTO
    {
        public int job_id { get; set; }
        public int account_id { get; set; }
        public int type_id { get; set; }
        public DateTime posted_date { get; set; }
        public DateTime deadline { get; set; }
    }

    public class JobNoId
    {
        public int account_id { get; set; }
        public int type_id { get; set; }
        public DateTime posted_date { get; set; }
        public DateTime deadline { get; set; }
    }

    public class CreateJob
    {
        [Required(ErrorMessage = "Please enter employer for create job!")]
        public int account_id { get; set; }

        [Required(ErrorMessage = "Please enter type of job!")]
        public int type_id { get; set; }

        [Required(ErrorMessage = "Please enter deadline for job!")]
        public DateTime deadline { get; set; }

    }

    public class UpdateJob
    {
        [Required (ErrorMessage = "Type job not null")]
        public int type_id { get; set; }

        [Required(ErrorMessage = "Deadline job not null")]
        public DateTime deadline { get; set; }

    }
}
