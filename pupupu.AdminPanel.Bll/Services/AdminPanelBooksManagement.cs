using pupupu.Bll.Models;
using pupupu.Bll.Services;
using pupupu.Core;

namespace AdminPanel.Bll;

public class AdminPanelBooksManagement: IAdminPanelBooksManagement
{
    private readonly IBookServiceInterface _bookService;

    public AdminPanelBooksManagement(IBookServiceInterface bookService)
    {
        _bookService = bookService;
    }

    public List<Book> GetAllBooks()
    {
        var books = _bookService.GetBooks();
        return books ?? new List<Book>();
    }

    public Errors CreateBook(Book book)
    {
        var newBook = _bookService.CreateBook(book);
        var errors = new Errors();
        if (newBook is null)
        {
            errors.AddMainError("Не удалось добавить книгу! Что-то пошло не так!");
        }
        return errors;
    }

    public Errors DeleteBook(int bookId)
    {
        _bookService.RemoveBook(bookId);
        return new Errors();
    }
}