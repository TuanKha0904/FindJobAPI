using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.Accounts
{
    public class AccountDTO
    {
        public int account_id { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public DateTime date_create { get; set; }
    }

    public class CreateAccount
    {
        [Required (ErrorMessage = "Please enter your email!")]
        [EmailAddress (ErrorMessage = "Invalid email address. Please Check!")]
        public string? email { get; set; }

        [Required (ErrorMessage = "Password can't be null!")]
        public string? password { get; set; }
    }

    public class UpdateAccount
    {
        [Required(ErrorMessage = "Password can't be null!")]
        public string? password { get; set; }

    }
}
