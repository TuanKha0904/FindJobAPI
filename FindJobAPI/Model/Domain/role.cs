using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.Domain
{
    public class role
    {
        [Key]
        public int role_id { get; set; }
        public string? role_name { get; set; }

        //navigation properties: one role has many account
        public List<account>? Account { get; set; } 
    }
}
