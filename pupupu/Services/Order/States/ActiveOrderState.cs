using pupupu.Common;
using pupupu.Services.Common;

namespace pupupu.Services.Order;
using pupupu.Models.BLL;

// Здесь могла бы быть какая-то логика, но мы используем анимечную модель, поэтому вся логика лежит в сервисах
// При таком подходе модели должны быть тонкими, то есть в идеале иметь только свойства. А логика идёт в сервисы
// Поэтому логика, связанная с изменениями статусов, будет лежать в сервисе заказов
public class ActiveOrderState: IOrderState
{
    public Errors Process(Order order)
    {
        var errors = new Errors();
        if (order.OrderStatus != OrderStatus.Active)
        {
            errors.AddMainError("Invalid order status");
        }
        order.OrderStatus = OrderStatus.Closed;
        return errors;
    }

    public Errors Cancel(Order order)
    {
        var errors = new Errors();
        if (order.OrderStatus != OrderStatus.Active)
        {
            errors.AddMainError("Invalid order status");
        }
        order.OrderStatus = OrderStatus.Canceled;
        return errors;
    }
}