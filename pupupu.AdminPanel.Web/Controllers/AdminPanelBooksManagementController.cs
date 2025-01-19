using AdminPanel.Bll;
using Microsoft.AspNetCore.Mvc;

namespace pupupu.Web.Controllers;

[Microsoft.AspNetCore.Components.Route("AdminPanel/BooksManagement")]
public class AdminPanelBooksManagementController: Controller
{
    private readonly IAdminPanelBooksManagement _adminPanelBooksManagement;

    public AdminPanelBooksManagementController(IAdminPanelBooksManagement adminPanelBooksManagement)
    {
        _adminPanelBooksManagement = adminPanelBooksManagement;
    }

    [HttpGet("List")]
    public IActionResult List()
    {
        var bookList = _adminPanelBooksManagement.GetAllBooks();
        return View(bookList);
    }

    [HttpPost("AdminPanelBooksManagement/Delete/{bookId}")]
    public IActionResult Delete(int bookId)
    {
        _adminPanelBooksManagement.DeleteBook(bookId);
        return RedirectToAction("List");
    }
}