using System.Threading.Tasks;
using Entities;

namespace Services
{
    public interface IUserService
    {
        Task<bool> IsDuplicateUser(string email);
        Task addUser(User user);
        Task<User> getUserToLogIn(string email, string password);
        Task updateUser(int id, User user);
        int PostPassword(string password);
        Task<User> getUserById(int id);
    }
}
