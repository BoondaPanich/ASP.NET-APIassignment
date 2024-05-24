using APIassignment;
using APIassignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("user")]

        public IActionResult UserLogin([FromBody] LoginModel model)
        {
            if (!(model.Username == "User1234" && model.Password == "User1234"))
            {
                return Unauthorized();
            }
            else
            {
                LoginModel user = new LoginModel();
                user.Username = model.Username;
                user.Password = model.Password;

                var token = Generate(user);
                return Ok(token);
            }
        }

        [AllowAnonymous]
        [HttpPost("admin")]

        public IActionResult AdminLogin([FromBody] LoginModel model)
        {
            if (!(model.Username == "Admin1234" && model.Password == "Admin1234"))
            {
                return Unauthorized();
            }
            else
            {
                LoginModel user = new LoginModel();
                user.Username = model.Username;
                user.Password = model.Password;

                var token = Generate(user);
                return Ok(token);
            }
        }


        private string Generate(LoginModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Hash, user.Password),
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}