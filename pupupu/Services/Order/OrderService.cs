using pupupu.Services.Interfaces;

namespace pupupu.Services.Order;

public class OrderService: IOrderService
{
    // TODO использовать rich модель для Order. тут получать заказ из репо и использовать декоратор
    public void ProcessOrder(int orderId)
    {
        throw new NotImplementedException();
    }

    public void CancelOrder(int orderId)
    {
        throw new NotImplementedException();
    }
}