using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySaverAPI.Models
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public string ExpenseName { get; set; }
        public string ExpenseCategory { get; set; }
        public decimal ExpenseAmount { get; set; }
        public string ExpenseDate { get; set; }
    }
}
