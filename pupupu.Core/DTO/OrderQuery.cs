namespace pupupu.Bll.Dto;

public class OrderQuery
{
    public int OrderId { get; set; }
    
    public string UserId { get; set; }

    public OrderQuery(int orderId = default, string userId = default)
    {
        this.OrderId = orderId;
        this.UserId = userId;
    }
}