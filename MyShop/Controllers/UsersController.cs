using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;
using Entities;
using DTO;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


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
        public async Task<ActionResult<object>> Login([FromBody] UserRegisterDTO loginDto)
        {
            // Validate user credentials
            var user = await userService.getUserToLogIn(loginDto.Email, loginDto.Password);
            if (user == null)
                return Unauthorized("Invalid credentials");

            // Generate JWT token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", user.UserId.ToString())
            };

            var config = HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? "YourSuperSecretKeyHere"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var issuer = config["Jwt:Issuer"] ?? "MyShopIssuer";

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = tokenString, user });
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
