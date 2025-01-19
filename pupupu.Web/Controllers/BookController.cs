using Microsoft.AspNetCore.Mvc;
using pupupu.Bll.Models;
using pupupu.Bll.Services;

namespace pupupu.Web.Controllers;

public class BookController: Controller
{
    private readonly IBookServiceInterface _bookService;

    public BookController(IBookServiceInterface bookService)
    {
        _bookService = bookService;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult List()
    {
        var books = _bookService.GetBooks() ?? new List<Book>();
        return View(books);
    }
}