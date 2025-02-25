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

        public async Task addUser(User user)
        {
            //var newUser =
            await ConectDb.Users.AddAsync(user);
            await ConectDb.SaveChangesAsync();
            //return newUser;
    

            //int numberOfUsers = System.IO.File.ReadLines("M:/web api/MyShop/MyShop/FileUser.txt").Count();
            //user.UserId = numberOfUsers + 1;
            //string userJson = JsonSerializer.Serialize(user);
            //System.IO.File.AppendAllText("M:/web api/MyShop/MyShop/FileUser.txt", userJson + Environment.NewLine);
        }

        public  async Task<User> getUserToLogIn(string Email, string Password)
        {
            //_logger.LogCritical($"Login attempted with User, {Email} and password {Password} ");
            return await ConectDb.Users.FirstOrDefaultAsync(user => user.Email == Email && user.Password == Password);
            //using (StreamReader reader = System.IO.File.OpenText("M:/web api/MyShop/MyShop/FileUser.txt"))
            //{
            //    string? currentUserInFile;
            //    while ((currentUserInFile = reader.ReadLine()) != null)
            //    {
            //        User user = JsonSerializer.Deserialize<User>(currentUserInFile);
            //        if (user.Email == UserName && user.Password == Password)
            //            return user;


            //    }
            //}
            //return null;
        }
        //public async Task<User> getUserById(int id)
        //{
        //    return await ConectDb.Users.FirstOrDefaultAsync(user => user.UserId == id);
        //}
        public async Task updateUser(int id, User Details)
        {
            Details.UserId = id;
            ConectDb.Update(Details);
            await ConectDb.SaveChangesAsync();
            //string textToReplace = string.Empty;
            //using (StreamReader reader = System.IO.File.OpenText("M:/web api/MyShop/MyShop/FileUser.txt"))
            //{
            //    string currentUserInFile;
            //    while ((currentUserInFile = reader.ReadLine()) != null)
            //    {

            //        User user = JsonSerializer.Deserialize<User>(currentUserInFile);
            //        if (user.UserId == id)
            //            textToReplace = currentUserInFile;
            //    }
            //}

            //if (textToReplace != string.Empty)
            //{
            //    string text = System.IO.File.ReadAllText("M:/web api/MyShop/MyShop/FileUser.txt");
            //    text = text.Replace(textToReplace, JsonSerializer.Serialize(Details));
            //    System.IO.File.WriteAllText("M:/web api/MyShop/MyShop/FileUser.txt", text);
            //}
        }
    }
}
