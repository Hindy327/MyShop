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
        //UserService userService = new UserService();
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "shabat", "shalom" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] UserRegisterDTO userDTO)//בלי הזמנות
        {
            User user = mapper.Map<UserRegisterDTO, User>(userDTO);
            await userService.addUser(user);
            return CreatedAtAction(nameof(Get), new { id = user.UserId }, user);

        }
        //[HttpPost]
        //[Route("login")]
        //public ActionResult PostLogin([FromBody] string userName, string password)
        //{
        //    using (StreamReader reader = System.IO.File.OpenText("M:/web api/MyShop/MyShop/FileUser.txt"))
        //    {
        //        string? currentUserInFile;
        //        while ((currentUserInFile = reader.ReadLine()) != null)
        //        {
        //            User user = JsonSerializer.Deserialize<User>(currentUserInFile);
        //            if (user.UserName == userName && user.Password == password)
        //                return Ok(user);


        //        }
        //    }
        //    return NoContent();



        //}
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
        public async Task Put( int id, [FromBody] UserRegisterDTO Details)
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

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
