using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        [Required]
        public string ISBN  { get; set; }
        public int PublishedYear { get; set; }
        public int Quantity { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }


    }
}
