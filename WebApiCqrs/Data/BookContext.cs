using Microsoft.EntityFrameworkCore;
using WebApiCqrs.Models;

namespace WebApiCqrs.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options)
            :base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
