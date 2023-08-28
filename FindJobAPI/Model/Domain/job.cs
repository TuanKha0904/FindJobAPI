﻿using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.Domain
{
    public class job
    {
        [Key]
        public int job_id { get; set; }

        //navigation properties: one job just one employer
        public int account_id { get; set; }
        public employer? employer { get; set; }

        //navigation properties: one job just one type
        public int type_id { get; set; }
        public type? type { get; set; }

        //navigation properties: one job has one industry
        public int industry_id { get; set; }
        public industry? industry { get; set; }
        public DateTime posted_date { get; set; }
        public DateTime deadline { get; set; }


        //navigation properties: one job has many recruitment
        public List<recruitment>? recruitment { get; set;}

        // navigation properties: one job has one jobdetail
        public List<job_detail>? job_detail { get; set; }
    }
}
