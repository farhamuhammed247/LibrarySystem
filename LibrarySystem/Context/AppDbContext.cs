using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Context
{
    public class AppDbContext : DbContext
    {

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }  
        public DbSet<Borrowing> Borrowings { get; set; }
        public DbSet<Member> Members { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=LibSystemInstant; Trusted_Connection=true; TrustServerCertificate=true");
        }
    }
}
