using pupupu.Services.Common;

namespace pupupu.Services.Order;
using pupupu.Models.BLL;


public class WaitingOrderState: IOrderState
{
    public void Process(Order order)
    {
        if (order.OrderStatus != OrderStatus.Waiting)
        {
            throw new InvalidOperationException("Invalid order status");
        }
        order.OrderStatus = OrderStatus.Active;
    }

    public void Cancel(Order order)
    {
        if (order.OrderStatus != OrderStatus.Waiting)
        {
            throw new InvalidOperationException("Invalid order status");
        }
        
        order.OrderStatus = OrderStatus.Canceled;
    }
}