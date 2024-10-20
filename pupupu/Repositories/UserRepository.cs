using pupupu.Common;
using pupupu.Models.DAL;
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

    public User GetUserByEmail(string email)
    {
        return _dbContext.Users.SingleOrDefault(u => u.Email == email);
    }

    public User CreateUser()
    {
        return new User();
    }

    public void AddUser(User user)
    {
        _dbContext.Users.Add(user);
    }
    
    public void RemoveUser(User user)
    {
        _dbContext.Remove(user);
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
}