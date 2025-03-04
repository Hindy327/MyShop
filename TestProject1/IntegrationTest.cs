using Entities;
using Reposetories;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

namespace TestProject1
{
    public class IntegrationTest : IClassFixture<DatabaseFixture>
    {
        private readonly _327725412WebApiContext _context;
        private readonly UserReposetory _reposetory;

        public IntegrationTest(DatabaseFixture fixture)
        {
            _context = fixture.Context; 
            _reposetory = new UserReposetory(_context);
        }

        [Fact]
        public async Task Get_ShouldReturnUser_WhenUserExists()
        {
         
            var user = new User { Email = "test1@example.com", Password = "password123", FirstName = "pppp", LastName = "vgfcgfc" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

           
            var retrievedUser = await _context.Users.FindAsync(user.UserId);

           
            Assert.NotNull(retrievedUser);
            Assert.Equal(user.Email, retrievedUser.Email);
            Assert.Equal(user.Password, retrievedUser.Password);
        }
        [Fact]
        public async Task Get_ShouldReturnNull_WhenUserDoesNotExist()
        {
            
            var retrievedUser = await _reposetory.getUserById(-1);

           
            Assert.Null(retrievedUser);
        }
        [Fact]
        public async Task Post_ShouldAddUser_WhenUserIsValid()
        {
           
            var user = new User { Email = "nnnewuser@example.com", Password = "Tt12345@@", FirstName = "gjh", LastName = "gjf" };


            
            var addedUser = await _reposetory.addUser(user);


           
            Assert.NotNull(addedUser);
            Assert.Equal(user.Email, addedUser.Email);
            Assert.True(addedUser.UserId > 0); 
        }


        [Fact]
        public async Task Login_ShouldReturnUser_WhenCredentialsAreValid()
        {
           
            var user = new User { Email = "ttestuser@example.com", Password = "Tt123456@@", FirstName = "gjh", LastName = "gjf" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

           
            var loggedInUser = await _reposetory.getUserToLogIn(user.Email, user.Password);


           
            Assert.NotNull(loggedInUser);
            Assert.Equal(user.Email, loggedInUser.Email);
        }

        [Fact]
        public async Task Login_ShouldReturnNull_WhenCredentialsAreInvalid()
        {
           
            var loggedInUser = await _reposetory.getUserToLogIn("unknown@example.com", "wrongpasswo");


          
            Assert.Null(loggedInUser);
        }

    }
}
