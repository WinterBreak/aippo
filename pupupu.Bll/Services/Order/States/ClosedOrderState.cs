using pupupu.Core;
using pupupu.Bll.Models;

namespace pupupu.Bll.Services;

public class ClosedOrderState: IOrderState
{
    public Errors Process(Order order)
    {
        var errors = new Errors();
        errors.AddMainError("Order is already closed");
        return errors;
    }

    public Errors Cancel(Order order)
    {
        var errors = new Errors();
        if (order.OrderStatus != OrderStatus.Closed)
        {
            errors.AddMainError("Invalid order status");
        }
        order.OrderStatus = OrderStatus.Canceled;
        return errors;
    }
}