using pupupu.Models.DAL;

namespace pupupu.Queries;

public class UserListQuery
{
    public UserListQuery()
    {
        UserType = UserType.None;
    }
    public UserType UserType { get; set; }
}