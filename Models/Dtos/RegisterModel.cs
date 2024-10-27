namespace NhaSachDaiThang_BE_API.Models.Dtos
{
    public class RegisterModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int RoleID { get; set; }
    }

}
