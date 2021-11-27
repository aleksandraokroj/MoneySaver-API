using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MoneySaverAPI.Models;
using System.Globalization;

namespace MoneySaverAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ExpensesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from dbo.Expense";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MoneySaverCon");
            SqlDataReader myReader;
            using(SqlConnection myCon=new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand=new SqlCommand(query, myCon))
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
        public JsonResult Post(Expense exp)
        {
            string query = @"insert into dbo.Expense values (@ExpenseName, @ExpenseCategory, @ExpenseAmount, @ExpenseDate)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MoneySaverCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ExpenseName", exp.ExpenseName);
                    myCommand.Parameters.AddWithValue("@ExpenseCategory", exp.ExpenseCategory);
                    myCommand.Parameters.AddWithValue("@ExpenseAmount", exp.ExpenseAmount);
                    myCommand.Parameters.AddWithValue("@ExpenseDate", exp.ExpenseDate);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Sucessfuly!");
        }

        [HttpPut]
        public JsonResult Put(Expense exp)
        {
            string query = @"update dbo.Expense set ExpenseName = @ExpenseName, ExpenseCategory=@ExpenseCategory, 
                            ExpenseAmount=@ExpenseAmount, ExpenseDate = @ExpenseDate
                            where ExpenseId = @ExpenseId";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MoneySaverCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ExpenseId", exp.ExpenseId);
                    myCommand.Parameters.AddWithValue("@ExpenseName", exp.ExpenseName);
                    myCommand.Parameters.AddWithValue("@ExpenseCategory", exp.ExpenseCategory);
                    myCommand.Parameters.AddWithValue("@ExpenseAmount", exp.ExpenseAmount);
                    myCommand.Parameters.AddWithValue("@ExpenseDate", exp.ExpenseDate);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated Sucessfuly!");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from dbo.Expense where ExpenseId = @ExpenseId";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MoneySaverCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ExpenseId", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted succesfully!");
        }


    }
}
