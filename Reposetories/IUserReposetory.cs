using Entities;

namespace Reposetories
{
    public interface IUserReposetory
    {
        Task<User> getUserById(int id);
        Task<User> getUserToLogIn(string Email, string Password);
        Task updateUser(int id, User Details);
        Task<User> addUser(User user);


    }
}