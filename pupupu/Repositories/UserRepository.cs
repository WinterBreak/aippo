using pupupu.Common;
using pupupu.Models;
using pupupu.Repositories.Interfaces;

namespace pupupu.Repositories;

public class UserRepository: IUserRepository
{
    private readonly BookOrderSystemContext _dbContext;

    public UserRepository(BookOrderSystemContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public IQueryable<User> GetAllUsers()
    {
        return _dbContext.Users;
    }

    public User GetUserById(int id)
    {
        return _dbContext.Users.SingleOrDefault(u => u.Id == id);
    }

    public User CreateUser()
    {
        return new User();
    }

    public void AddUser(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }
    
    public void RemoveUser(User user)
    {
        _dbContext.Remove(user);
        _dbContext.SaveChanges();
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
}