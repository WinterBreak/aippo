using pupupu.Core;
using pupupu.Bll.Dto;
using pupupu.Dal.Repositories;
using pupupu.Bll.Services;
using pupupu.Bll.Models;
using DAL = pupupu.Dal.Models;

namespace pupupu.Web.Services;

public class OrderService: IOrderService
{
    private readonly IOrderHistoryRepository _orderHistoryRepository;
    private readonly IBookRepository _bookRepository;

    public OrderService(IOrderHistoryRepository orderHistoryRepository
        , IBookRepository bookRepository)
    {
        _orderHistoryRepository = orderHistoryRepository;
        _bookRepository = bookRepository;
    }
    
    // TODO использовать rich модель для Order. тут получать заказ из репо и использовать декоратор
    public Errors ProcessOrder(int orderId)
    {
        var dalOrder = _orderHistoryRepository.GetOrderHistoryById(orderId);
        ArgumentNullException.ThrowIfNull(dalOrder);
        
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

    public Errors CancelOrder(int orderId)
    {
        var dalOrder = _orderHistoryRepository.GetOrderHistoryById(orderId);
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
}