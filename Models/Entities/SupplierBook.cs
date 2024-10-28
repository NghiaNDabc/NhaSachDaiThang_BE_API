namespace NhaSachDaiThang_BE_API.Models.Entities
{
    public partial class SupplierBook
    {
        public int SupplierId { get; set; }
        public int BookId { get; set; }
        public int Quanlity { get; set; }
        public DateTime? SupplyDate { get; set; } // Ngày cung cấp

        public decimal? SupplyPrice { get; set; } // Giá cung cấp

        public virtual Supplier? Supplier { get; set; }
        public virtual Book? Book { get; set; }
    }
}
