using System;
using Reposetories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Text.Json;
using Zxcvbn;

namespace Services
{
    public class UserService : IUserService
    {
        IUserReposetory userReposetory;
        
        public UserService(IUserReposetory _userReposetory)
        {
            userReposetory = _userReposetory;
    }
        public async  Task addUser(User user)
        {
           await  userReposetory.addUser(user);
        }

        public async Task<User> getUserToLogIn(string Email, string Password)
        {
            return await userReposetory.getUserToLogIn(Email, Password);

        }
        public async Task updateUser(int id, User Details)
        {
            await userReposetory.updateUser(id, Details);

        }
        public int PostPassword(string Password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(Password);
            return result.Score;

        }

    }
}
