using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pupupu.Models.DAL;

namespace pupupu.Common;

public class BookOrderSystemContext: IdentityDbContext<User>
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