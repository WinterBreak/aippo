namespace pupupu.Bll.Models;

public class OrderItem
{
    public int BookId { get; set; }
    
    public int Amount { get; set; }

    public OrderItem() {}

    public OrderItem(int bookId)
    {
        this.BookId = bookId;
        this.Amount = 1;
    }
}