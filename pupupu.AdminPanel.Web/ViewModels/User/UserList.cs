using AdminPanel.Dal;

namespace AdminPanel.Web
{
    public class UserList(List<User> users)
    {
        public List<User> Users { get; set; } = users;
    }
}