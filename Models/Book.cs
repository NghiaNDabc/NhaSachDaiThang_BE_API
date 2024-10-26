﻿using System;
using System.Collections.Generic;

namespace NhaSachDaiThang_BE_API.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string? Title { get; set; }

    public string? Author { get; set; }

    public string? Publisher { get; set; }

    public DateOnly? PublishDate { get; set; }

    public decimal? Price { get; set; }

    public decimal? PurchasePrice { get; set; }

    public int? Quantity { get; set; }

    public int? CategoryId { get; set; }

    public string? Description { get; set; }

    public bool? IsDel { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string? ModifyBy { get; set; }

    public string? MainImage { get; set; }

    public virtual ICollection<BookImage> BookImages { get; set; } = new List<BookImage>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();
}
