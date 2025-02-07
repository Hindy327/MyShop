using Entities;
using Reposetories;
using Moq;
using Moq.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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




    }

    
}