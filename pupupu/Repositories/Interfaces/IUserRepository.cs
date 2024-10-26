using pupupu.Models.DAL;

namespace pupupu.Repositories.Interfaces;

public interface IUserRepository
{
    IQueryable<User> GetAllUsers();
    
    User GetUserByEmail(string email);
    
    User GetUserById(string id);
    
    void AddUser(User user);
    
    User CreateUser();
    
    void RemoveUser(User user);
    
    void SaveChanges();
}