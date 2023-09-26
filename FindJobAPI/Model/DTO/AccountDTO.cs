using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Model.DTO
{
    public class AllAccountDTO
    {
        public string? UID { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? DateCreate { get; set; }
    }

    public class Login
    {
        public string? UID { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Photo { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public class Infor
    {
        public string? Name { get; set; }

        [Phone (ErrorMessage ="số điện thoại không đúng")]
        public string? PhoneNumber { get; set; }
    }

    public class Photo
    {
        public string? PhotoUrl { get; set; }
    }

    public class UpdateAccount
    {
        public string? Name { get; set; }
        public string? email { get; set; }
        public string? avatar { get; set; }
    }
}
