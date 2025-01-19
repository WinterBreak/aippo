using Microsoft.EntityFrameworkCore;
using pupupu.Web.Common;
using pupupu.Dal.Models;

namespace pupupu.Dal.Repositories;

public class OrderHistoryRepository: IOrderHistoryRepository
{
    private readonly BookOrderSystemContext _dbContext;

    public OrderHistoryRepository(BookOrderSystemContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IQueryable<OrderHistory> GetAllOrderHistories()
    {
        return _dbContext.OrderHistories
            .Include(o => o.BooksToOrderHistoryLinks)
            .ThenInclude(oh => oh.Book)
            .ThenInclude(oh => oh.Author);
    }

    public IQueryable<OrderHistory> GetOrderHistoriesByUserId(string userId)
    {
        return GetAllOrderHistories().Where(o => o.UserId == userId);
    }

    public OrderHistory GetOrderHistoryById(int id)
    {
        return GetAllOrderHistories().SingleOrDefault(o => o.Id == id);
    }

    public OrderHistory CreateOrderHistory()
    {
        return new OrderHistory();
    }

    public void AddOrderHistory(OrderHistory orderHistory)
    {
        _dbContext.BooksToOrderHistoryLinks.AddRange(orderHistory.BooksToOrderHistoryLinks);
        _dbContext.Add(orderHistory);
    }

    public void RemoveOrderHistory(OrderHistory orderHistory)
    {
        _dbContext.BooksToOrderHistoryLinks.RemoveRange(orderHistory.BooksToOrderHistoryLinks);
        _dbContext.OrderHistories.Remove(orderHistory);
    }

    public void SaveChanges() => _dbContext.SaveChanges();
}