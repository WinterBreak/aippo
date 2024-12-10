using pupupu.Services.Common;

namespace pupupu.Services.Order;
using pupupu.Models.BLL;

public class ClosedOrderState: IOrderState
{
    public void Process(Order order)
    {
        throw new InvalidOperationException("Order is already closed"); // тут, возможно, лучше ошибки возвращать. но лень
    }

    public void Cancel(Order order)
    {
        if (order.OrderStatus != OrderStatus.Closed)
        {
            throw new InvalidOperationException("Invalid order status");
        }
        order.OrderStatus = OrderStatus.Canceled;
    }
}