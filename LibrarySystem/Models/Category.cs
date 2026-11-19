using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
