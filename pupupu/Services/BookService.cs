using pupupu.Repositories.Interfaces;
using pupupu.Services.Interfaces;
using System.Linq;
using pupupu.Models.Bll;
using DAL = pupupu.Models.DAL;

namespace pupupu.Services;

public class BookService: IBookServiceInterface
{
    private readonly IBookRepository _bookRepository;
    
    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public List<Book> GetBooks()
    {
        var dalBooks = _bookRepository
            .GetAllBooks().OrderBy(b => b.Title);
        return dalBooks.Select(b => new Book(b)).ToList();
    }

    public List<Book> GetBooksByAuthorId(int authorId)
    {
        var dalBooks = _bookRepository
            .GetBooksByAuthorId(authorId).OrderBy(b => b.Title);
        return dalBooks.Select(b => new Book(b)).ToList();
    }

    public Book GetBookById(int bookId)
    {
        var dalBook = _bookRepository.GetBookById(bookId);
        return new Book(dalBook);
    }

}