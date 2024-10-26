using System;
using System.Collections.Generic;

namespace NhaSachDaiThang_BE_API.Models;

public partial class Promotion
{
    public int PromotionId { get; set; }

    public string? Name { get; set; }

    public decimal? Discount { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string? ModifyBy { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
