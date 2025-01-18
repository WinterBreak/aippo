using pupupu.Bll.Models;
using pupupu.Core;

namespace AdminPanel.Bll;

public interface IAdminPanelBooksManagement
{
    List<Book> GetAllBooks();

    Errors CreateBook(Book book);

    Errors DeleteBook(int bookId);
}