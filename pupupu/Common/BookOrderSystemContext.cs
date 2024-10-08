using Microsoft.EntityFrameworkCore;
using pupupu.Models;

namespace pupupu.Common;

public class BookOrderSystemContext: DbContext
{
    public BookOrderSystemContext(DbContextOptions<BookOrderSystemContext> options)
        : base(options)
    {
        
    }
    
    public DbSet<Book> Books { get; set; }

    public DbSet<Author> Authors { get; set; }

    public DbSet<OrderHistory> OrderHistories { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<BooksToOrderHistoryLinks> BooksToOrderHistoryLinks { get; set; }
}