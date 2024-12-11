namespace pupupu.Models.DAL;

public class BooksToOrderHistoryLinks
{
    public int OrderId { get; set; }
    
    public int BookId { get; set; }
    
    public virtual OrderHistory OrderHistory { get; set; }
    public virtual Book Book { get; set; }
}