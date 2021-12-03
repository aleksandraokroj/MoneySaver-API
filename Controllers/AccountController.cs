using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MoneySaverAPI.Models;

namespace MoneySaverAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountController(IConfiguration configuration, IPasswordHasher<User> passwordHasher)
        {
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser(User user)
        {
            string query = @"insert into dbo.Users values (@Email,  @FirstName, @LastName, @PasswordHash)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MoneySaverCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    var hashedPassword = _passwordHasher.HashPassword(user, user.Password);
                    myCommand.Parameters.AddWithValue("@Email", user.Email);
                    myCommand.Parameters.AddWithValue("@FirstName", user.FirstName);
                    myCommand.Parameters.AddWithValue("@LastName", user.LastName);
                    myCommand.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return Ok();

        }
    }
}
