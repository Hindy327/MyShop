using System;
using Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;

namespace Reposetories
{
    public class UserReposetory : IUserReposetory
    {
        private readonly ILogger<UserReposetory> _logger;
        _327725412WebApiContext ConectDb;
        public UserReposetory(_327725412WebApiContext _327725412WebApiContext, ILogger<UserReposetory> logger)
        {
            ConectDb = _327725412WebApiContext;
            _logger = logger;
        }

        public UserReposetory(_327725412WebApiContext _327725412WebApiContext)
        {
            ConectDb = _327725412WebApiContext;
        }

        public async Task<User> addUser(User user)
        {
            var newUser =await ConectDb.Users.AddAsync(user);
            await ConectDb.SaveChangesAsync();
            return user;
        }
        public  async Task<User> getUserToLogIn(string Email, string Password)
        {
            return await ConectDb.Users.FirstOrDefaultAsync(user => user.Email == Email && user.Password == Password);
        }
        public async Task<User> getUserById(int id)
        {
            return await ConectDb.Users.FirstOrDefaultAsync(u => u.UserId==id);
        }
        public async Task updateUser(int id, User Details)
        {
            Details.UserId = id;
            ConectDb.Users.Update(Details);
            await ConectDb.SaveChangesAsync();
        }
    }
}
