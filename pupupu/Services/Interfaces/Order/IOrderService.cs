namespace pupupu.Services.Interfaces;

public interface IOrderService
{
    void ProcessOrder(int orderId);
    
    void CancelOrder(int orderId);
}