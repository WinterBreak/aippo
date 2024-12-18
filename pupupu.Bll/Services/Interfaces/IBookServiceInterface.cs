using pupupu.Bll.Models;

namespace pupupu.Bll.Services;

public interface IBookServiceInterface
{
    List<Book> GetBooks();

    List<Book> GetBooksByAuthorId(int authorId);

    Book GetBookById(int bookId);

    Book CreateBook(Book newBook);

    void RemoveBook(int bookId);

}