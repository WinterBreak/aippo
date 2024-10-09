namespace pupupu.Models;

public class OrderHistory
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime OrderEndDate { get; set; }

    public int Status { get; set; }
}
