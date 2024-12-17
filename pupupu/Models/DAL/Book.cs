namespace pupupu.Models.DAL;

public class Book
{
    public Book()
    {
        this.BooksToOrderHistoryLinks = new HashSet<BooksToOrderHistoryLinks>();
    }
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public int AuthorId { get; set; }

    public virtual Author Author { get; set; }
    
    public virtual ICollection<BooksToOrderHistoryLinks> BooksToOrderHistoryLinks { get; set; }
}