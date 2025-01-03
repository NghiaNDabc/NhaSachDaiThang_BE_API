﻿namespace NhaSachDaiThang_BE_API.Models.Dtos
{
    public class BookDto
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

        public int? Quantity { get; set; }

        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int? BookCoverTypeId { get; set; }
        public string? BookCoverTypeName{ get; set; }
        public int? LanguageId { get; set; }
        public string? LanguageName { get; set; }
        public string? Description { get; set; }
        public string? Promotion { get; set; }
        public DateTime? PromotionEndDate { get; set; }

        public bool? IsDel { get; set; }


        public string? CreatedBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string? ModifyBy { get; set; }

        public string? MainImage { get; set; }
        public string? AdditionalImages { get; set; }
    }
}
