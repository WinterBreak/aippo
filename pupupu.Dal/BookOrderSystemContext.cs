using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pupupu.Dal.Models;
using pupupu.Dal.Models;

namespace pupupu.Web.Common;

public class BookOrderSystemContext: IdentityDbContext<User>
{
    public BookOrderSystemContext(DbContextOptions<BookOrderSystemContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
         builder.Entity<BooksToOrderHistoryLinks>()
            .HasKey(p => new { p.BookId, p.OrderId });
         builder.Entity<User>().ToTable("Users");
         builder.Entity<IdentityUserLogin<string>>(entity =>
         {
             entity.HasKey(l => new { l.LoginProvider, l.ProviderKey });
         });
         builder.Entity<IdentityUserRole<string>>(entity =>
         {
             entity.HasKey(ur => new { ur.UserId, ur.RoleId });
         });

         builder.Entity<IdentityUserClaim<string>>(entity =>
         {
             entity.HasKey(uc => uc.Id);
         });

         builder.Entity<IdentityUserToken<string>>(entity =>
         {
             entity.HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });
         });
    }
    
    public DbSet<Book> Books { get; set; }

    public DbSet<Author> Authors { get; set; }

    public DbSet<OrderHistory> OrderHistories { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<BooksToOrderHistoryLinks> BooksToOrderHistoryLinks { get; set; }
}