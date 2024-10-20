using pupupu.Common;
using pupupu.Models.DAL;
using pupupu.Repositories.Interfaces;

namespace pupupu.Repositories;

public class BookRepository: IBookRepository
{
    private readonly BookOrderSystemContext _dbContext;

    public BookRepository(BookOrderSystemContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IQueryable<Book> GetAllBooks()
    {
        return _dbContext.Books;
    }

    public Book GetBookById(int id)
    {
        return _dbContext.Books.SingleOrDefault(b => b.Id == id);
    }

    public Book CreateBook()
    {
        return new Book();
    }

    public void AddBook(Book book)
    {
        _dbContext.Books.Add(book);
        _dbContext.SaveChanges(); // TODO убери потом. я протупила, так лучше не делать. будет много транзакций
    }

    public void RemoveBook(Book book)
    {
        _dbContext.Books.Remove(book);
        _dbContext.SaveChanges();
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
}