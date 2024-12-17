using pupupu.Common;
using pupupu.Services.Common;

namespace pupupu.Services.Order;
using pupupu.Models.BLL;

public class CancelOrderState: IOrderState
{
    public Errors Process(Order order)
    {
        var errors = new Errors();
        errors.AddMainError("Order is already cancelled");
        return errors;
    }

    public Errors Cancel(Order order)
    {
        var errors = new Errors();
        errors.AddMainError("Order is already cancelled");
        return errors;
    }
}