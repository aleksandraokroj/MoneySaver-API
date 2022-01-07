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
using Microsoft.EntityFrameworkCore;

namespace MoneySaverAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly MoneySaverDbContext _context;

        public ExpensesController(MoneySaverDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpense()
        {
            return await _context.Expense.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses(int id)
        {
            return await _context.Expense.Where(e => e.UserId == id ).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(Expense exp)
        {
            _context.Expense.Add(exp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpense", new { id = exp.ExpenseId }, exp);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(int id, Expense exp)
        {
            System.Diagnostics.Debug.WriteLine(id);
            System.Diagnostics.Debug.WriteLine(exp.ExpenseId);
            if (id != exp.ExpenseId)
            {
                return BadRequest();
            }
            _context.Entry(exp).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
    

        [HttpDelete("{id}")]
        public async Task<ActionResult<Expense>> DeleteExpense(int id)
        {
            var exp = await _context.Expense.FindAsync(id);
            if (exp == null)
            {
                return NotFound();
            }

            _context.Expense.Remove(exp);
            await _context.SaveChangesAsync();

            return exp;
        }

        private bool ExpenseExists(int id)
        {
            return _context.Expense.Any(e => e.ExpenseId == id);
        }


    }
}
