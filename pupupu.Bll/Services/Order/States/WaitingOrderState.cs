using pupupu.Core;
using pupupu.Bll.Models;

namespace pupupu.Bll.Services;


public class WaitingOrderState: IOrderState
{
    public Errors Process(Order order)
    {
        var errors = new Errors();
        if (order.OrderStatus != OrderStatus.Waiting)
        {
            errors.AddMainError("Invalid order status");
        }
        order.OrderStatus = OrderStatus.Active;
        return errors;
    }

    public Errors Cancel(Order order)
    {
        var errors = new Errors();
        if (order.OrderStatus != OrderStatus.Waiting)
        {
            errors.AddMainError("Invalid order status");
        }
        
        order.OrderStatus = OrderStatus.Canceled;
        return errors;
    }
}