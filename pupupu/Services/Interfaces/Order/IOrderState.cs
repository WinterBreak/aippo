using pupupu.Common;

namespace pupupu.Services.Common;
using pupupu.Models.BLL;

public interface IOrderState
{
    Errors Process(Order order);
    
    Errors Cancel(Order order);
}