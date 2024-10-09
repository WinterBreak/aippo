using pupupu.Common;
using pupupu.Models;
using pupupu.Repositories.Interfaces;

namespace pupupu.Repositories;

// метобы — заглушки. реализовать. предлагаю тебе сделать это самой.
// так познакомишься с linq
public class BookRepository: IBookRepository
{
    // в каждом репозитории определяется объект контекста бд
    private readonly BookOrderSystemContext _dbContext;

    public BookRepository(BookOrderSystemContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IQueryable<Book> GetAllBooks()
    {
        throw new NotImplementedException();
    }

    public Book GetBookById(int id)
    {
        throw new NotImplementedException();
    }

    public Book CreateBook()
    {
        throw new NotImplementedException();
    }

    public void AddBook(Book book)
    {
        throw new NotImplementedException();
    }

    public void RemoveBook(Book book)
    {
        throw new NotImplementedException();
    }

    public void SaveChanges()
    {
        throw new NotImplementedException();
    }
}