using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.Domain
{
    public class account
    {
        [Key]
        public int account_id { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }

        public DateTime date_create { get; set; }

        //navigation properties: one account just has seeker or employer
        public List<seeker>? seekers { get; set; }
        public List<employer>? employers { get; set; }
    }
}
