namespace NhaSachDaiThang_BE_API.Models.Entities
{
    public partial class Supplier
    {
        public int SupplierId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }   
        public string? Address { get; set; }
        public virtual ICollection<SupplierBook> SupplierBooks { get; set; } = new List<SupplierBook>();
    }
}
