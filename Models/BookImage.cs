using System;
using System.Collections.Generic;

namespace NhaSachDaiThang_BE_API.Models;

public partial class BookImage
{
    public int ImageId { get; set; }

    public int BookId { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string? ModifyBy { get; set; }

    public virtual Book Book { get; set; } = null!;
}
