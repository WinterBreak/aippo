namespace pupupu.Bll.Dto;

public class UserListQuery
{
    public UserListQuery()
    {
        UserType = UserType.None;
    }
    public UserType UserType { get; set; }
}