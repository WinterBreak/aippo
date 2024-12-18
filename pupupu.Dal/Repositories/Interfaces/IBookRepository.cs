using pupupu.Dal.Models;

namespace pupupu.Dal.Repositories;

public interface IBookRepository
{
    IQueryable<Book> GetAllBooks();

    IQueryable<Book> GetBooksByAuthorId(int authorId);
    
    IQueryable<Book> GetBooksByIds(IEnumerable<int> ids);
    
    Book GetBookById(int id);

    Book CreateBook();
    
    void AddBook(Book book);
    
    void RemoveBook(Book book);
    
    void SaveChanges();
}