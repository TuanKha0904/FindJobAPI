using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.Accounts
{
    public class AccountDTO
    {
        public string? email { get; set; }
        public string? password { get; set; }
        public DateTime date_create { get; set; }
        public int role_id { get; set; }
    }

    public class CreateAccount
    {
        [Required (ErrorMessage = "Please enter your email!")]
        public string? email { get; set; }

        [Required (ErrorMessage = "Password can't be null!")]
        public string? password { get; set; }

        [Required (ErrorMessage = "Choose your target!")]
        public int role_id { get; set; }
    }

    public class UpdateAccount
    {
        [Required(ErrorMessage = "Password can't be null!")]
        public string? password { get; set; }

        [Required(ErrorMessage = "Choose your target!")]
        public int role_id { get; set;}
    }
}
