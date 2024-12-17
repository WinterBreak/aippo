namespace pupupu.Models.DAL;

public class BooksToOrderHistoryLinks
{
    public BooksToOrderHistoryLinks(int bookId, int orderId)
    {
        this.BookId = bookId;
        this.OrderId = orderId;
    }

    public BooksToOrderHistoryLinks(Book book, OrderHistory orderHistory)
    {
        this.Book = book;
        this.OrderHistory = orderHistory;
    }
    
    public int OrderId { get; set; }
    
    public int BookId { get; set; }
    
    public virtual OrderHistory OrderHistory { get; set; }
    public virtual Book Book { get; set; }
}