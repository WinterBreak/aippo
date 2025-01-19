using pupupu.Core;
using pupupu.Bll.Models;
using pupupu.Bll.Dto;

namespace pupupu.Bll.Services;

public interface IOrderService
{
    List<OrderItem> GetOrderItemsFromSession();

    List<Book> GetOrderFromSession();

    void SaveOrderToSession(List<OrderItem> order);

    void AddToOrder(int bookId);

    void ClearOrder();

    void RemoveFromOrder(int bookId);
    
    Errors ProcessOrder(OrderQuery query);
    
    Errors CancelOrder(OrderQuery query);
    
    List<Order> GetOrders();
    
    Order GetOrderById(int orderId);
    
    Errors AddOrder(Order order);
    
    Errors EditOrder(Order order);
    
    Errors DeleteOrder(int orderId);
}