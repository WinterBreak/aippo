using pupupu.Models.DAL;

namespace pupupu.Repositories.Interfaces;

public interface IBookRepository
{
    IQueryable<Book> GetAllBooks();

    IQueryable<Book> GetBooksByAuthorId(int authorId);
    
    Book GetBookById(int id);

    Book CreateBook();
    
    void AddBook(Book book);
    
    void RemoveBook(Book book);
    
    void SaveChanges();
}