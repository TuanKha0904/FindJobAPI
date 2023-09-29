using FindJobAPI.Model.Domain;

namespace FindJobAPI.Model.DTO
{
   public class Create
    {
        public int job_id { get; set;}
        public string? name { get; set;}
        public string? email { get; set;}
        public string? phone { get; set;}
        public string? birthday { get; set;} 
        public string? address { get; set;}
        public string? major { get; set;}
        public string? experience { get; set;}
        public string? education { get; set;}
        public string? skills { get; set;}
    }

    public class Get
    {
        public string? name { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? birthday { get; set; }
        public string? address { get; set; }
        public string? major { get; set; }
        public string? experience { get; set; }
        public string? education { get; set; }
        public string? skills { get; set; }

    }
}
