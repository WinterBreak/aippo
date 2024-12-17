using pupupu.Models.DAL;

namespace pupupu.Repositories.Interfaces;

public interface IOrderHistoryRepository
{
    IQueryable<OrderHistory> GetAllOrderHistories();

    IQueryable<OrderHistory> GetOrderHistoriesByUserId(int userId);
    
    OrderHistory GetOrderHistoryById(int id);

    OrderHistory CreateOrderHistory();
    
    void AddOrderHistory(OrderHistory book);
    
    void RemoveOrderHistory(OrderHistory book);
    
    void SaveChanges();
}