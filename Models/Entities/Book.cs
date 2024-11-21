using System;
using System.Collections.Generic;

namespace NhaSachDaiThang_BE_API.Models.Entities;

public partial class Book
{
    public int BookId { get; set; }

    public string? Title { get; set; }

    public string? Author { get; set; }

    public string? Publisher { get; set; }

    public int? PublishYear { get; set; }
    public int? PageCount { get; set; } 
    public string? Size { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Price { get; set; }

    //public decimal? PurchasePrice { get; set; }

    public int? Quantity { get; set; }

    public int? CategoryId { get; set; }

    public string? Description { get; set; }
    public string? Promotion { get; set; }
    public DateTime? PromotionEndDate { get; set; }

    public bool? IsDel { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string? ModifyBy { get; set; }

    public string? MainImage { get; set; }
    public string? AdditionalImages { get; set; }
    public int? BookCoverTypeId { get; set; }
    public int? LanguageId { get; set; }

    public virtual BookCoverType? BookCoverType { get; set; }
    public virtual Language? Language { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    public virtual ICollection<SupplierBook> SupplierBooks { get; set; } = new List<SupplierBook>();
}
