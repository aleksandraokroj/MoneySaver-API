using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySaverAPI.Models
{
    public partial class MoneySaverDbContext : DbContext
    {
        public MoneySaverDbContext(DbContextOptions<MoneySaverDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Expense> Expense { get; set; }
        public DbSet<Goal> Goal { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>(entity =>
            {
                entity.Property(e => e.ExpenseName)
                .IsRequired();

                entity.Property(e => e.ExpenseCategory)
                .IsRequired()
                .HasMaxLength(50);

                entity.Property(e => e.ExpenseAmount)
                .IsRequired();

                entity.Property(e => e.ExpenseDate)
                .IsRequired();
            });

            modelBuilder.Entity<Goal>(entity =>
            {
                entity.Property(g => g.GoalName)
                    .IsRequired();

                entity.Property(g => g.GoalCategory)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(g => g.GoalAmount)
                    .IsRequired();

                entity.Property(g => g.GoalDate)
                    .IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Email)
                .IsRequired();

                entity.Property(u => u.FirstName)
                .HasMaxLength(50);

                entity.Property(u => u.LastName)
                .HasMaxLength(50);

                entity.Property(u => u.PasswordHash)
                .IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

