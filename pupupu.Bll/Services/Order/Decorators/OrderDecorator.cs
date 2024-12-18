using pupupu.Core;
using pupupu.Bll.Models;

namespace pupupu.Bll.Services;

public abstract class OrderDecorator: Order //TODO щас лень, но лучше тогда какой-то абстрактный класс замутить
{
    protected Order _order;

    public OrderDecorator(Order order)
    {
        _order = order;
    }
    
    public virtual Errors Process()
    {
        return _order.ProcessOrderState();
    }

    public virtual Errors Cancel()
    {
       return _order.CancelOrder();
    }
}