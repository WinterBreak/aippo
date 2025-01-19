using Microsoft.AspNetCore.Mvc;
using pupupu.Bll.Dto;
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

    [HttpGet]
    public IActionResult List()
    {
        var orders = _orderService.GetOrders();
        return View(orders);
    }

    [HttpPost]
    public IActionResult ProcessOrder()
    {
        _orderService.ProcessOrder(new OrderQuery(default, default));
        return RedirectToAction("List");
    }

    [HttpGet]
    public IActionResult Order()
    {
        var order = _orderService.GetOrderFromSession();
        return View("Order", order);
    }
    
    [HttpPost]
    public IActionResult RemoveFromOrder(int bookId)
    {
        _orderService.RemoveFromOrder(bookId);
        return RedirectToAction("Order");
    }
    
    [HttpPost]
    public IActionResult AddToOrder(int bookId)
    {
        var book = _bookService.GetBookById(bookId);
        if (book == null)
        {
            return NotFound("Книга не найдена! Что-то пошло не так(");
        }
        var books = _bookService.GetBooks();
        _orderService.AddToOrder(bookId);
        return RedirectToAction(nameof(BookController.List), "Book", books);
    }
    
    [HttpPost]
    public IActionResult ClearOrder()
    {
        _orderService.ClearOrder();
        return RedirectToAction("Order");
    }
}