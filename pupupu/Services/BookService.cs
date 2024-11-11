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
            .GetAllBooks().OrderBy(b => b.Name);
        return dalBooks.Select(b => new Book(b)).ToList();
    }

    public List<Book> GetBooksByAuthorId(int authorId)
    {
        var dalBooks = _bookRepository
            .GetBooksByAuthorId(authorId).OrderBy(b => b.Name);
        return dalBooks.Select(b => new Book(b)).ToList();
    }

    public Book GetBookById(int bookId)
    {
        var dalBook = _bookRepository.GetBoookById(bookId)
        return new Book(dalBook);
    }

    // здесь методы могут возвращать только bll (бизнес-логика) модели
    // bll модель может повторять dal, но как правило, она потолще
    // поскольку в неё можно напихать чуть ли не всё, что нужно функциональности (я про данные)
    // например, OrderHistory на уровне бизнес логики не существует. bll модель будет называться просто Order
    // и в ней будут лежать все данные, необходимые для заказа, а не так, как сейчас реализовано на уровне бд
}