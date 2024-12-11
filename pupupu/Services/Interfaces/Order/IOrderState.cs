namespace pupupu.Services.Common;
using pupupu.Models.BLL;

public interface IOrderState
{
    void Process(Order order);
    
    void Cancel(Order order);
}