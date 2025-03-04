using Entities;
using Reposetories;
using Moq;
using Moq.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
namespace TestProject1
{

    public class UnitTest1
    {

        [Fact]
        public async Task GetUser_ValidCredentials_ReturnsUser()
        {
           
            var user = new User { Email = "Hindy@gmail.com", Password = "327725412" };
            var mockContext = new Mock<_327725412WebApiContext>();
            var users = new List<User>() { user };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
            var userRepository = new UserReposetory(mockContext.Object);
            var result = await userRepository.getUserToLogIn(user.Email, user.Password);
            Assert.Equal(user, result);


        }
        [Fact]
        public async Task Get_UserExists_ReturnsUser()
        {
           
            var mockContext = new Mock<_327725412WebApiContext>();
            var userToReturn = new User { UserId = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };

            var users = new List<User>() { userToReturn };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
           
            mockContext.Setup(m => m.Users.FindAsync(It.IsAny<int>())).ReturnsAsync(userToReturn);

            var Reposetory = new UserReposetory(mockContext.Object);

            
            var result = await Reposetory.getUserById(1);

            
            Assert.NotNull(result);
            Assert.Equal(1, result.UserId);
            Assert.Equal("John", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            Assert.Equal("john.doe@example.com", result.Email);
        }
        [Fact]
        public async Task Get_UserDoesNotExist_ReturnsNull()
        {
           
            var mockContext = new Mock<_327725412WebApiContext>();
            var users = new List<User>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
           
            mockContext.Setup(m => m.Users.FindAsync(It.IsAny<int>())).ReturnsAsync((User)null);

            var Reposetory = new UserReposetory(mockContext.Object);

           
            var result = await Reposetory.getUserById(999);

           
            Assert.Null(result);
        }
        [Fact]
        public async Task Post_FailedToAddUser_ThrowsException()
        {
           
            var mockContext = new Mock<_327725412WebApiContext>();
            var userToAdd = new User
            {
                UserId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "securepassword"
            };
            var users = new List<User>() { userToAdd };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
           
            mockContext.Setup(m => m.Users.AddAsync(It.IsAny<User>(), default)).ThrowsAsync(new System.Exception("Failed to add user"));

            var Reposetory = new UserReposetory(mockContext.Object);

           
            await Assert.ThrowsAsync<System.Exception>(async () => await Reposetory.addUser(userToAdd)); 
        }
        [Fact]
        public async Task Put_UserDoesNotExist_ThrowsException()
        {
           
            var mockContext = new Mock<_327725412WebApiContext>();
            var userToUpdate = new User
            {
                UserId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "securepassword"
            };
            var users = new List<User>() { userToUpdate };
            mockContext.Setup(x => x.Users).ReturnsDbSet(users);
           
            mockContext.Setup(m => m.Users.Update(It.IsAny<User>()));

           
            mockContext.Setup(m => m.SaveChangesAsync(default)).ThrowsAsync(new System.Exception("Failed to save changes"));

            var Reposetory = new UserReposetory(mockContext.Object);

            
            await Assert.ThrowsAsync<System.Exception>(async () => await Reposetory.updateUser(1, userToUpdate)); 
        }







    }

    
}