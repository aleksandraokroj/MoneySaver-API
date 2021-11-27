using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MoneySaverAPI.Models;

namespace MoneySaverAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public GoalsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from dbo.Goal";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MoneySaverCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Goal goal)
        {
            string query = @"insert into dbo.Goal values (@GoalName, @GoalCategory, @GoalAmount, @GoalDate)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MoneySaverCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@GoalName", goal.GoalName);
                    myCommand.Parameters.AddWithValue("@GoalCategory", goal.GoalCategory);
                    myCommand.Parameters.AddWithValue("@GoalAmount", goal.GoalAmount);
                    myCommand.Parameters.AddWithValue("@GoalDate", goal.GoalDate);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Sucessfully!");
        }


        [HttpPut]
        public JsonResult Put(Goal goal)
        {
            string query = @"update dbo.Goal set GoalName=@GoalName, GoalCategory=@GoalCategory, GoalAmount=@GoalAmount, GoalDate=@GoalDate
                              where GoalId=@GoalId";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MoneySaverCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@GoalId", goal.GoalId);
                    myCommand.Parameters.AddWithValue("@GoalName", goal.GoalName);
                    myCommand.Parameters.AddWithValue("@GoalCategory", goal.GoalCategory);
                    myCommand.Parameters.AddWithValue("@GoalAmount", goal.GoalAmount);
                    myCommand.Parameters.AddWithValue("@GoalDate", goal.GoalDate);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated Sucessfully!");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from dbo.Goal where GoalId=@GoalId";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MoneySaverCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@GoalId", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Sucessfully!");
        }

    }
}
