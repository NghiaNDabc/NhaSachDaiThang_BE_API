namespace NhaSachDaiThang_BE_API.Models.Entities
{
    public class Language
    {
        public int LanguageId { get; set; } 
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Book>  Books { get; set; } = new List<Book>();
    }
}
