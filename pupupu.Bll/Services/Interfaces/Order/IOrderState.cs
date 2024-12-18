using pupupu.Core;
using pupupu.Bll.Models;

namespace pupupu.Bll.Services;


public interface IOrderState
{
    Errors Process(Order order);
    
    Errors Cancel(Order order);
}