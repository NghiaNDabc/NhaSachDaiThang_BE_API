namespace NhaSachDaiThang_BE_API.Models.Dtos
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }

        public string? Name { get; set; }
        public bool? IsDel { get; set; }
        public string? Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string? ModifyBy { get; set; }
    }
}
