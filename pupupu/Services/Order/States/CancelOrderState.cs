using pupupu.Services.Common;

namespace pupupu.Services.Order;
using pupupu.Models.BLL;

public class CancelOrderState: IOrderState
{
    public void Process(Order order)
    {
        throw new InvalidOperationException("Order is already cancelled");
    }

    public void Cancel(Order order)
    {
        throw new InvalidOperationException("Order is already cancelled");
    }
}