namespace pupupu.Services.Order.Decorators;
using pupupu.Models.BLL;

public class SmsNotificationOrderDecoration: OrderDecorator
{
    public override void Process(Order order)
    {
        base.Process(order);
        SendSmsNotification();
    }

    public override void Cancel(Order order)
    {
        base.Cancel(order);
        SendSmsNotification();
    }
    
    private void SendSmsNotification()
    {
        // TODO попробовать Twilio. С Identity должно работать
        Console.WriteLine("Sending sms notification");
    }
}