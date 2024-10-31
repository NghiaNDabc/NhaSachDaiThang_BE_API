using System;
using System.Collections.Generic;

namespace NhaSachDaiThang_BE_API.Models.Entities;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? Name { get; set; }
    public bool? IsDel { get; set; }
    public string? Description { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string? ModifyBy { get; set; }
    public int? ParentCategoryID { get; set; }
    public virtual Category? ParentCategory { get; set; }
    public virtual ICollection<Category> SubCategories { get; set; } = new List<Category>();
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
