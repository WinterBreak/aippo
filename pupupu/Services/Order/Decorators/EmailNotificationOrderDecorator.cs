using pupupu.Common;

namespace pupupu.Services.Order.Decorators;
using pupupu.Models.BLL;

public class EmailNotificationOrderDecorator: OrderDecorator
{
    public EmailNotificationOrderDecorator(Order order): base(order)
    { }
    
    public override Errors Process()
    {
        base.Process();
        return SendEmailNotification();
    }

    public override Errors Cancel()
    {
        base.Cancel();
        return SendEmailNotification();
    }

    private Errors SendEmailNotification()
    {
        // TODO попробовать MailKit
        Console.WriteLine("Sending email notification");
        return new Errors();
    }
}