namespace NhaSachDaiThang_BE_API.Models.Dtos
{
    public class ChangePassDto
    {
        public string Email {  get; set; } 
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
