using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.Domain
{
    public class account
    {
        [Key]
        public string? email { get; set; }
        public string? password { get; set; }

        public DateTime date_create { get; set; }

        //navigation properties: one role has many acount
        public int role_id { get; set; }
        public role? role {get; set;}

        //navigation properties: one account just has seeker or employer
        public List<seeker>? seekers { get; set; }
        public List<employer>? employers { get; set; }
    }
}
