using pupupu.Common;
using pupupu.Services.Common;

namespace pupupu.Services.Order;
using pupupu.Models.BLL;


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