using pupupu.Core;
using pupupu.Bll.Models;
using pupupu.Bll.Dto;

namespace pupupu.Bll.Services;

public interface IOrderService
{
    Errors ProcessOrder(int orderId);
    
    Errors CancelOrder(int orderId);
    
    List<Order> GetOrders(OrderQuery query);
    
    Order GetOrderById(int orderId);
    
    Errors AddOrder(Order order);
    
    Errors EditOrder(Order order);
    
    Errors DeleteOrder(int orderId);
}