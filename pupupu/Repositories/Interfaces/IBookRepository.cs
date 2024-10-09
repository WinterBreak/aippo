using pupupu.Models;

namespace pupupu.Repositories.Interfaces;

// методы для обращения к бд. базовые операции я определю, остальное по мере надобности
public interface IBookRepository
{
    IQueryable<Book> GetAllBooks();
    
    Book GetBookById(int id);

    Book CreateBook();
    
    void AddBook(Book book);
    
    void RemoveBook(Book book);
    
    // этот метод сохраняет изменения в бд
    // то есть его нужно обязательно вызывать, когда заканчиваешь мунипуляции с данными
    void SaveChanges();
}