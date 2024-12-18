using pupupu.Core;
using pupupu.Bll.Services;
using DAL = pupupu.Dal.Models;

namespace pupupu.Bll.Models;

// Rich модель слегка просочилась, но почему бы и нет
public class Order
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    
    public OrderStatus OrderStatus { get; set; }
    
    public DateTime OrderDate { get; set; }

    public DateTime OrderEndDate { get; set; }
    
    public List<Book> OrderedBooks { get; set; }
    
    private IOrderState _state { get; set; }

    public Order(DAL.OrderHistory fromValue)
    {
        this._state = new ActiveOrderState();
        this.Id = fromValue.Id;
        this.UserId = fromValue.UserId;
        this.OrderDate = fromValue.OrderDate;
        this.OrderEndDate = fromValue.OrderEndDate;
        this.OrderStatus = (OrderStatus)fromValue.Status;
        this.OrderedBooks = fromValue.BooksToOrderHistoryLinks
            .Select(b => new Book(b.Book)).ToList();
    }

    public Order()
    {
        this._state = new ActiveOrderState();
        this.OrderStatus = OrderStatus.Waiting;
    }

    public Errors ProcessOrderState()
    {
        return this._state.Process(this);
    }

    public Errors CancelOrder()
    {
        return this._state.Process(this);
    }
}