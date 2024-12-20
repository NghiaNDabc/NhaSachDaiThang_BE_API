﻿using System;
using System.Collections.Generic;

namespace NhaSachDaiThang_BE_API.Models.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }
    public string? RecipientName { get; set; }  

    public DateTime? OrderDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? Status { get; set; }
    public string? PaymentMethod {  get; set; }
    public string? ShippingAddress { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public DateTime? DeliveredDate { get; set; }
    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string? ModifyBy { get; set; }

    public virtual User? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    //public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    //public virtual ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();
}
