﻿namespace NhaSachDaiThang_BE_API.Models.Entities
{
    public partial class SupplierBook
    {
        public int SupplierBookId { get; set; }
        public int SupplierId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public string? Note { get; set; }
       // public decimal? DiscountAmount { get; set; }
        public DateTime? SupplyDate { get; set; } // Ngày cung cấp

        public decimal? SupplyPrice { get; set; } // Giá cung cấp
        public DateTime? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string? ModifyBy { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual Book? Book { get; set; }
    }
}
