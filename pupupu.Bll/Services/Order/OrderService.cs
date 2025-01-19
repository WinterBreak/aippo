using pupupu.Core;
using pupupu.Bll.Dto;
using pupupu.Dal.Repositories;
using pupupu.Bll.Services;
using pupupu.Bll.Models;
using DAL = pupupu.Dal.Models;
using Microsoft.AspNetCore.Http;

namespace pupupu.Bll;

public class OrderService: IOrderService
{
    private const string OrderSessionKey = "Order";
    private readonly IOrderHistoryRepository _orderHistoryRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public OrderService(IOrderHistoryRepository orderHistoryRepository
        , IBookRepository bookRepository
        , IHttpContextAccessor httpContextAccessor)
    {
        _orderHistoryRepository = orderHistoryRepository;
        _bookRepository = bookRepository;
        _httpContextAccessor = httpContextAccessor;
    }
    
    // TODO использовать rich модель для Order. тут получать заказ из репо и использовать декоратор
    public List<OrderItem> GetOrderItemsFromSession()
    {
        var session = _httpContextAccessor.HttpContext.Session;
        var order = session.Get<List<OrderItem>>(OrderSessionKey) ?? new List<OrderItem>();
        return order;
    }

    public void SaveOrderToSession(List<OrderItem> order)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        session.Set(OrderSessionKey, order);
    }
    
    public void AddToOrder(int bookId)
    {
        var order = GetOrderItemsFromSession();
        var existingItem = order.FirstOrDefault(x => x.BookId == bookId);

        if (existingItem != null)
        {
            existingItem.Amount++;
        }
        else
        {
            var newItem = new OrderItem(bookId);
            order.Add(newItem);
        }

        SaveOrderToSession(order);
    }

    public void ClearOrder()
    {
        var session = _httpContextAccessor.HttpContext.Session;
        session.Remove(OrderSessionKey);
    }

    public void RemoveFromOrder(int bookId)
    {
        var order = GetOrderItemsFromSession();
        var itemToRemove = order.FirstOrDefault(x => x.BookId == bookId);

        if (itemToRemove != null)
        {
            order.Remove(itemToRemove);
            SaveOrderToSession(order);
        }
    }

    public Errors ProcessOrder(OrderQuery query)
    {
        var dalOrder = _orderHistoryRepository.GetOrderHistoryById(query.OrderId);
        if (dalOrder is null)
        {
            dalOrder = AddNewDalOrder(query);
        }
        
        var order = new Order(dalOrder);
        var errors = order.ProcessOrderState();
        if (errors.HasErrors)
        {
            return errors;
        }
        
        var orderDecorator = new EmailNotificationOrderDecorator(new SmsNotificationOrderDecoration(order));
        errors = orderDecorator.Process();
        return errors;
    }

    public Errors CancelOrder(OrderQuery query)
    {
        var dalOrder = _orderHistoryRepository.GetOrderHistoryById(query.OrderId);
        ArgumentNullException.ThrowIfNull(dalOrder);
        
        var order = new Order(dalOrder);
        var errors = order.CancelOrder();
        if (errors.HasErrors)
        {
            return errors;
        }
        
        var orderDecorator = new EmailNotificationOrderDecorator(new SmsNotificationOrderDecoration(order));
        errors = orderDecorator.Process();
        return errors;
    }

    public List<Order> GetOrders(OrderQuery query)
    {
        var dalOrders = _orderHistoryRepository.GetAllOrderHistories();
        return dalOrders.Select(o => new Order(o)).ToList();
    }

    public Order GetOrderById(int orderId)
    {
        var dalOrder = _orderHistoryRepository.GetOrderHistoryById(orderId);
        ArgumentNullException.ThrowIfNull(dalOrder);
        
        return new Order(dalOrder);
    }

    public Errors AddOrder(Order order) // ваще не факт, что это работает
    {
        var newDalOrder = _orderHistoryRepository.CreateOrderHistory();
        newDalOrder.UserId = order.UserId;
        newDalOrder.OrderDate = order.OrderDate;
        newDalOrder.Status = (int)order.OrderStatus;
        _orderHistoryRepository.AddOrderHistory(newDalOrder);

        var dalBooks = GetDalOrderedBook(order.OrderedBooks);
        newDalOrder.BooksToOrderHistoryLinks = dalBooks
            .Select(b => new DAL.BooksToOrderHistoryLinks(b, newDalOrder)).ToList();
        
        _orderHistoryRepository.SaveChanges();
        return new Errors(); // ну тут предполагается какая-то валидация, которую я не придумала
    }

    public Errors EditOrder(Order order)
    {
        var dalUpdatingOrder = _orderHistoryRepository.GetOrderHistoryById(order.Id);
        ArgumentNullException.ThrowIfNull(dalUpdatingOrder);
        
        dalUpdatingOrder.OrderDate = order.OrderDate;
        dalUpdatingOrder.Status = (int)order.OrderStatus;
        dalUpdatingOrder.BooksToOrderHistoryLinks = GetDalOrderedBook(order.OrderedBooks)
            .Select(b => new DAL.BooksToOrderHistoryLinks(b, dalUpdatingOrder)).ToList();
        
        return new Errors();
    }

    public Errors DeleteOrder(int orderId)
    {
        var dalOrder = _orderHistoryRepository.GetOrderHistoryById(orderId);
        ArgumentNullException.ThrowIfNull(dalOrder);
        
        _orderHistoryRepository.RemoveOrderHistory(dalOrder);
        _orderHistoryRepository.SaveChanges();
        return new Errors();
    }
    
    private List<DAL.Book> GetDalOrderedBook(IEnumerable<Book> orderedBooks)
    {
        var bookIds = orderedBooks.Select(b => b.Id);
        return _bookRepository.GetBooksByIds(bookIds).ToList();
    }

    private DAL.OrderHistory AddNewDalOrder(OrderQuery query)
    {
        var newDalOrder = _orderHistoryRepository.CreateOrderHistory();
        newDalOrder.OrderDate = DateTime.UtcNow;
        newDalOrder.UserId = query.UserId;
        var orderItems = GetOrderItemsFromSession();
        newDalOrder.BooksToOrderHistoryLinks = orderItems
            .Select(x => new DAL.BooksToOrderHistoryLinks(x.BookId, query.OrderId))
            .ToList();
        return newDalOrder;
    }
}