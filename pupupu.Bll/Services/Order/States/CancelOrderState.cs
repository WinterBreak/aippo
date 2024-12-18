using pupupu.Core;
using pupupu.Bll.Models;

namespace pupupu.Bll.Services;

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