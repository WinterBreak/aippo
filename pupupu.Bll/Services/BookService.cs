using pupupu.Bll.Models;
using pupupu.Dal.Repositories;
using pupupu.Bll.Services;

namespace pupupu.Bll.Services;

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

    public Book CreateBook(Book newBook)
    {
        var dalBook = _bookRepository.CreateBook();
        
        dalBook.Title = newBook.Title;
        dalBook.Description = newBook.Description;
        dalBook.AuthorId = newBook.AuthorId;

        _bookRepository.AddBook(dalBook);
        _bookRepository.SaveChanges();
        return new Book(dalBook);
    }

    public void RemoveBook(int bookId)
    {
        var dalBook = _bookRepository.GetBookById(bookId);
        _bookRepository.RemoveBook(dalBook);
        _bookRepository.SaveChanges();
    }

}