using pupupu.Services.Common;

namespace pupupu.Services.Order;
using pupupu.Models.BLL;

// Здесь могла бы быть какая-то логика, но мы используем анимечную модель, поэтому вся логика лежит в сервисах
// При таком подходе модели должны быть тонкими, то есть в идеале иметь только свойства. А логика идёт в сервисы
// Поэтому логика, связанная с изменениями статусов, будет лежать в сервисе заказов
public class ActiveOrderState: IOrderState
{
    public void Process(Order order)
    {
        if (order.OrderStatus != OrderStatus.Active)
        {
            throw new InvalidOperationException("Invalid order status");
        }
        order.OrderStatus = OrderStatus.Closed;
    }

    public void Cancel(Order order)
    {
        if (order.OrderStatus != OrderStatus.Active)
        {
            throw new InvalidOperationException("Invalid order status");
        }
        order.OrderStatus = OrderStatus.Canceled;
    }
}