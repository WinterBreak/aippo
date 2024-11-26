using pupupu.Models.Bll;

namespace pupupu.Services.Interfaces;

public interface IBookServiceInterface
{
    List<Book> GetBooks();

    List<Book> GetBooksByAuthorId(int authorId);

    Book GetBookById(int bookId);

    Book CreateBook(Book newBook);

    void RemoveBook(int bookId);

}