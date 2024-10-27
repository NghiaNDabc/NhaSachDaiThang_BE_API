using System;
using System.Collections.Generic;

namespace NhaSachDaiThang_BE_API.Models.Entities;

public partial class Voucher
{
    public int VoucherId { get; set; }

    public string? Code { get; set; }

    public decimal? Discount { get; set; }

    public DateOnly? ExpirationDate { get; set; }

    public decimal? MinOrderValue { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string? ModifyBy { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
