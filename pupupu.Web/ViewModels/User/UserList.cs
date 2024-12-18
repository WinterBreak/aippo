using DAL = pupupu.Dal.Models;

namespace pupupu.Web.ViewModels.User
{
    public class UserList(List<DAL.User> users)
    {
        public List<DAL.User> Users { get; set; } = users;
    }
}