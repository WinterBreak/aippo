using pupupu.Services.Common;

namespace pupupu.Services.Order.Decorators;
using pupupu.Models.BLL;

public abstract class OrderDecorator : IOrderState
{
    protected Order _order;
    public virtual void Process(Order order)
    {
        this._order = order;
        order.ProcessOrderState();
    }

    public virtual void Cancel(Order order)
    {
        this._order = order;
        order.CancelOrder();
    }
}