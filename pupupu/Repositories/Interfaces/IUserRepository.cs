using pupupu.Models;

namespace pupupu.Repositories.Interfaces;

public interface IUserRepository
{
    IQueryable<User> GetAllUsers();
    
    User GetUserById(int id);
    
    void AddUser(User user);
    
    User CreateUser();
    
    void RemoveUser(User user);
    
    void SaveChanges();
}