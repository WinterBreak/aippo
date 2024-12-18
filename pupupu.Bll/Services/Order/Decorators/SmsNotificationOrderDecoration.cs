using pupupu.Core;
using pupupu.Bll.Models;

namespace pupupu.Bll.Services;

public class SmsNotificationOrderDecoration: OrderDecorator
{
    public SmsNotificationOrderDecoration(Order order) : base(order)
    {
        
    }
    
    public override Errors Process()
    {
        base.Process();
        return SendSmsNotification();
    }

    public override Errors Cancel()
    {
        base.Cancel();
        return SendSmsNotification();
    }
    
    private Errors SendSmsNotification()
    {
        // TODO попробовать Twilio. С Identity должно работать
        Console.WriteLine("Sending sms notification");
        return new Errors();
    }
}