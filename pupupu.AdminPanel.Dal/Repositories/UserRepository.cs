namespace AdminPanel.Dal;

public class UserRepository: IUserRepository
{
    private readonly AdminPanelContext _dbContext;

    public UserRepository(AdminPanelContext dbContext)
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

    public User GetUserById(string id)
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