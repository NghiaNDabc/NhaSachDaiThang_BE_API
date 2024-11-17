namespace NhaSachDaiThang_BE_API.Models.Entities
{
    public class BookCoverType
    {
        public int BookCoverTypeId { get; set; } // Primary Key
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
