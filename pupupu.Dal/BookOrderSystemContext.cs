using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pupupu.Dal.Models;
using pupupu.Dal.Models;

namespace pupupu.Web.Common;

public class BookOrderSystemContext: DbContext
{
    public BookOrderSystemContext(DbContextOptions<BookOrderSystemContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(BookOrderSystemContext).Assembly);
        builder.HasDefaultSchema("public");
         builder.Entity<BooksToOrderHistoryLinks>()
            .HasKey(p => new { p.BookId, p.OrderId });
    }
    
    public DbSet<Book> Books { get; set; }

    public DbSet<Author> Authors { get; set; }

    public DbSet<OrderHistory> OrderHistories { get; set; }

    public DbSet<BooksToOrderHistoryLinks> BooksToOrderHistoryLinks { get; set; }
}