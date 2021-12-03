using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MoneySaverAPI.Models;

namespace MoneySaverAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MoneySaverDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountController(MoneySaverDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                PasswordHash = dto.Password
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);

            newUser.PasswordHash = hashedPassword;
                _context.User.Add(newUser);
                _context.SaveChangesAsync();     

            return Ok();

        }
        [HttpPost("login")]
        public ActionResult Login(LoginDto dto)
        {
            var user = _context.User.First(u => u.Email == dto.Email);

            if(user is null)
            {
                return BadRequest("Invalid email or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                return BadRequest("Invalid email or password");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(15);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();

            return Ok(tokenHandler.WriteToken(token));
        }
    }
}
