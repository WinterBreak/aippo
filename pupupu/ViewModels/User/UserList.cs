using DAL = pupupu.Models.DAL;

namespace pupupu.ViewModels.User
{
    public class UserList(List<DAL.User> users)
    {
        public List<DAL.User> Users { get; set; } = users;
    }
}