using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySaverAPI.Models
{
    public class Goal
    {
        public int GoalId { get; set; }
        public int UserId { get; set; }
        public string GoalName { get; set; }
        public string GoalCategory { get; set; }
        public decimal GoalAmount { get; set; }
        public decimal GoalProgress { get; set; }
        public DateTime GoalDate { get; set; }
    }
}
