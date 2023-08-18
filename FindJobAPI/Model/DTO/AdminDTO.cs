using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.DTO
{
    public class AdminDTO
    {
        public int Admin_Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    public class AdminNoId
    {
        [Required(ErrorMessage = "Please enter username!")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Please enter password!")]
        public string? Password { get; set; }
    }

    public class UpdateAdmin
    {
        [Required(ErrorMessage = "Please enter password!")]
        public string? Password { get; set; }
    }
}
