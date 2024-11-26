namespace NhaSachDaiThang_BE_API.Models.Dtos
{
    public class ApiResult
    {
        public int? Count { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ErrMessage { get; set; }
        public object Data { get; set; }
        
    }
}
