using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.Roles
{
    public class RoleDTO
    {
        public int role_id { get; set; }
        public string? role_name { get; set;}
    }

    public class RoleNoId
    {
        [Required (ErrorMessage = "Please enter role name!")]
        public string? role_name { get; set; }
    }
}
