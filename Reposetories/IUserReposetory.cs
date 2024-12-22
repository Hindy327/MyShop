using Entities;

namespace Reposetories
{
    public interface IUserReposetory
    {
        Task addUser(User user);
        Task<User> getUserToLogIn(string Email, string Password);
        Task updateUser(int id, User Details);
    }
}