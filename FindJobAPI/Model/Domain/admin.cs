using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.Domain
{
    public class admin
    {
        [Key]
        public int admin_id { get; set; }
        [Key]
        public string? username { get; set; }
        public string? password { get; set; }
    }
}
