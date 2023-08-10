namespace FindJobAPI.Model.Admins
{
    public class AdminDTO
    {
        public int Admin_Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }   
    }

    public class AdminNoId
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    public class UpdateAdmin
    {
        public string? Password { get; set; }
    }
}
