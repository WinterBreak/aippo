using pupupu.Repositories.Interfaces;
using pupupu.Services.Interfaces;

namespace pupupu.Services;

public class BookService: IBookServiceInterface
{
    // здесь определяются объекты, которые нужны для работы сервиса
    // пока что это только репозиторий. но в будущем может понадобиться ещё какой-нибудь сервис, к примеру
    private readonly IBookRepository _bookRepository;
    
    // объекты инициализируются через конструктор
    // для того, чтобы это работало, нужно настроить зависимости через контейнеры 
    // я это сделаю, просто объясняю, шо к чему
    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    // здесь методы могут возвращать только bll (бизнес-логика) модели
    // bll модель может повторять dal, но как правило, она потолще
    // поскольку в неё можно напихать чуть ли не всё, что нужно функциональности (я про данные)
    // например, OrderHistory на уровне бизнес логики не существует. bll модель будет называться просто Order
    // и в ней будут лежать все данные, необходимые для заказа, а не так, как сейчас реализовано на уровне бд
}