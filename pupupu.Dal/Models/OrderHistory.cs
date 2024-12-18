namespace pupupu.Dal.Models;

public class OrderHistory
{
    public OrderHistory()
    {
        this.BooksToOrderHistoryLinks = new HashSet<BooksToOrderHistoryLinks>();
    }
    
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime OrderEndDate { get; set; }

    public int Status { get; set; }
    
    public virtual ICollection<BooksToOrderHistoryLinks> BooksToOrderHistoryLinks { get; set; }
}
