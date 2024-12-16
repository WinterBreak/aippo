namespace pupupu.Services.Order.Decorators;
using pupupu.Models.BLL;

public class EmailNotificationOrderDecorator: OrderDecorator
{
    public override void Process(Order order)
    {
        base.Process(order);
        SendEmailNotification();
    }

    public override void Cancel(Order order)
    {
        base.Cancel(order);
        SendEmailNotification();
    }

    private void SendEmailNotification()
    {
        // TODO попробовать MailKit
        Console.WriteLine("Sending email notification");
    }
}