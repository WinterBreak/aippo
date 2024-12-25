using pupupu.Models.Bll;

namespace pupupu.Services.Interfaces;

public interface IBookServiceInterface
{
    public List<Book> GetBooks();
    
    public List<Book> GetBooksByAuthorId(int authorId);

    public Book GetBookById(int bookId);

}