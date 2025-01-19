using AdminPanel.Bll;
using Microsoft.AspNetCore.Mvc;
using pupupu.Bll.Dto;
using pupupu.Bll.Services;

namespace pupupu.AdminPanel.Web.Controllers;

[Route("AdminPanel/OrdersManagement")]
public class AdminPanelOrdersManagementController: Controller
{
    private readonly IAdminPanelBooksManagement _adminPanelBooksManagement;
    private readonly IOrderService _orderService;

    public AdminPanelOrdersManagementController(IAdminPanelBooksManagement adminPanelBooksManagement
        , IOrderService orderService)
    {
        _adminPanelBooksManagement = adminPanelBooksManagement;
        _orderService = orderService;
    }
    
    [HttpGet("List")]
    public IActionResult List()
    {
        var orderList = _orderService.GetOrders();
        return View(orderList);
    }
    
    [Route("/AdminPanel/OrdersManagement/CancelOrder/{orderId}")]
    public IActionResult CancelOrder(int orderId)
    {
        var query = new OrderQuery(orderId: orderId);
        _orderService.CancelOrder(query);
        return RedirectToAction(nameof(List));
    }

    [Route("/AdminPanel/OrdersManagement/ProcessOrder/{orderId}")]
    public IActionResult ProcessOrder(int orderId)
    {
        var query = new OrderQuery(orderId: orderId);
        _orderService.ProcessOrder(query);
        return RedirectToAction(nameof(List));
    }
}