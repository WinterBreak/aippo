using pupupu.Common;
using pupupu.Queries;

namespace pupupu.Services.Interfaces;
using Models.BLL;

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