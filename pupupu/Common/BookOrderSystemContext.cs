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
}