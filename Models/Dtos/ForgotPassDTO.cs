namespace NhaSachDaiThang_BE_API.Models.Dtos
{
    public class ForgotPassDTO
    {
        public string email { get; set; }
        public string otpCode { get; set; }
        public string newPass { get; set; }
    }
}
