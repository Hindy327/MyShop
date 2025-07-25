using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;
using Entities;
using DTO;
using AutoMapper;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService userService;
        IMapper mapper;
        public UsersController(IUserService _userService, IMapper mapper)
        {
            userService = _userService;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] UserRegisterDTO userDTO)
        {
            User user = mapper.Map<UserRegisterDTO, User>(userDTO);
            User u = await userService.getUserToLogIn(user.Email, user.Password);
            if (u != null)
            {
                return Conflict("Duplicate user");
            }
            // Generate salt and hash password
            string salt = MyShop.SecurityHelper.GenerateSalt();
            user.Salt = salt;
            user.Password = MyShop.SecurityHelper.HashPassword(user.Password, salt);
            await userService.addUser(user);
            return CreatedAtAction(nameof(getUserById), new { id = user.UserId }, user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<User>> PostLogin([FromQuery] string Email, string Password)//רק id
        {
            User user = await userService.getUserToLogIn(Email, Password);
            if (user != null)
                return Ok(user);

            return NoContent();
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] UserRegisterDTO Details)
        {
            User user = mapper.Map<UserRegisterDTO, User>(Details);
            await userService.updateUser(id, user);
        }

        [HttpPost]
        [Route("password")]
        public int PostPassword([FromBody] string Password)
        {
           return userService.PostPassword(Password);
        }
        [HttpGet("{id}")]
        public async Task<User> getUserById(int id)
        {
            User u= await userService.getUserById(id);
            return u;
        }
    }
}
