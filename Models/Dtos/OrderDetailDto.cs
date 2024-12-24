namespace NhaSachDaiThang_BE_API.Models.Dtos
{
    public class OrderDetailDto
    {
        public int OrderDetailId { get; set; }

        public int? OrderId { get; set; }

        public int? BookId { get; set; }
        public string? MainImage { get; set; }  
        public string? Title { get; set; }  

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }
    }
}
