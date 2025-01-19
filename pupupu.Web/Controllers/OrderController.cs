using Microsoft.AspNetCore.Mvc;
using pupupu.Bll.Services;

namespace pupupu.Web.Controllers;

public class OrderController: Controller
{
    private readonly IOrderService _orderService;
    private readonly IBookServiceInterface _bookService;

    public OrderController(IOrderService orderService, IBookServiceInterface bookService)
    {
        _orderService = orderService;
        _bookService = bookService;
    }
    
    [HttpPost]
    public IActionResult AddToOrder(int bookId)
    {
        var book = _bookService.GetBookById(bookId);
        if (book == null)
        {
            return NotFound("Книга не найдена! Что-то пошло не так(");
        }
        
        _orderService.AddToOrder(bookId);
        return RedirectToAction();
    }
}