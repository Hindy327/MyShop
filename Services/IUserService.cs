using Entities;

namespace Services
{
    public interface IUserService
    {
        Task<User> addUser(User user);
        Task<User> getUserToLogIn(string Email, string Password);
        int PostPassword(string Password);
        Task updateUser(int id, User Details);
        Task<User> getUserById(int id);
    }
    //change from Nechami
}