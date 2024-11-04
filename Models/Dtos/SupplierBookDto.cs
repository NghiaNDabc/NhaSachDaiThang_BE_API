﻿using NhaSachDaiThang_BE_API.Models.Entities;

namespace NhaSachDaiThang_BE_API.Models.Dtos
{
    public class SupplierBookDto
    {
        public int SupplierId { get; set; }
        public int BookId { get; set; }
        public int Quanlity { get; set; }
        public DateTime? SupplyDate { get; set; } // Ngày cung cấp

        public decimal? SupplyPrice { get; set; } // Giá cung cấp
        public DateTime? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string? ModifyBy { get; set; }
    }
}