using System;
using Reposetories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Text.Json;
using Zxcvbn;
using Microsoft.Extensions.Logging;

namespace Services
{
    public class UserService : IUserService
    {
        IUserReposetory userReposetory;
        private readonly ILogger<UserReposetory> _logger;

        public UserService(IUserReposetory _userReposetory, ILogger<UserReposetory> _logger)
        {
            userReposetory = _userReposetory;
            this._logger = _logger;
    }
        public async  Task<User> addUser(User user)
        {
          return await userReposetory.addUser(user);
           
        }
        public async Task<User> getUserById(int id)
        {
           return await userReposetory.getUserById(id);
        }
        public async Task<User> getUserToLogIn(string Email, string Password)
        {
            var u= await userReposetory.getUserToLogIn(Email, Password);
            if(u != null)
            {
                _logger.LogCritical($"Login attempted with User, {Email} and password {Password} ");
            }
            return u;

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
