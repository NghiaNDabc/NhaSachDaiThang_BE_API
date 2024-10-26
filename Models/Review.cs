using System;
using System.Collections.Generic;

namespace NhaSachDaiThang_BE_API.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int? CustomerId { get; set; }

    public int? BookId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime ReviewDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string? ModifyBy { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Customer? Customer { get; set; }
}
