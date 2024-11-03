namespace NhaSachDaiThang_BE_API.Models.Entities
{
    public partial class SupplierDto
    {
        public int SupplierId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string? ModifyBy { get; set; }
    }
}
