using System;
using Homework_19.Entities;
using Homework_19.Enums;
using Homework_19.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Homework_19
{
    public class AppContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Money> Money { get; set; }

        public AppContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=bank;Trusted_Connection=True;");
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=bank;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasData(
                new Client { Id = 1, Name = "Orson Avery", DepartmentRefId = 1 },
                new Client { Id = 2, Name = "Whoopi Franks", DepartmentRefId = 1 },
                new Client { Id = 3, Name = "Kermit Olsen", DepartmentRefId = 1 },
                new Client { Id = 4, Name = "Yoshi Gallagher", DepartmentRefId = 1 },
                new Client { Id = 5, Name = "Hamish Cole", DepartmentRefId = 1 },
                new Client { Id = 6, Name = "Emma Sharp", DepartmentRefId = 1 },
                new Client { Id = 7, Name = "Hilary Coleman", DepartmentRefId = 1 },
                new Client { Id = 8, Name = "Vance Barlow", DepartmentRefId = 1 },
                new Client { Id = 9, Name = "Felicia Sutton", DepartmentRefId = 1 },
                new Client { Id = 10, Name = "Dexter Huber", DepartmentRefId = 1 },
                new Client { Id = 11, Name = "Wynne Gilliam", DepartmentRefId = 2 },
                new Client { Id = 12, Name = "Austin Wilkins", DepartmentRefId = 2 },
                new Client { Id = 13, Name = "Tiger Whitehead", DepartmentRefId = 2 },
                new Client { Id = 14, Name = "Ora Weaver", DepartmentRefId = 2 },
                new Client { Id = 15, Name = "Ray Lyons", DepartmentRefId = 2 },
                new Client { Id = 16, Name = "Alden Ingram", DepartmentRefId = 2 },
                new Client { Id = 17, Name = "Gabriel Perez", DepartmentRefId = 2 },
                new Client { Id = 18, Name = "Xanthus Knapp", DepartmentRefId = 2 },
                new Client { Id = 19, Name = "Juliet Clark", DepartmentRefId = 2 },
                new Client { Id = 20, Name = "Oscar Coleman", DepartmentRefId = 2 },
                new Client { Id = 21, Name = "Myles Marsh", DepartmentRefId = 3 },
                new Client { Id = 22, Name = "Trevor Mercado", DepartmentRefId = 3 },
                new Client { Id = 23, Name = "Leo Little", DepartmentRefId = 3 },
                new Client { Id = 24, Name = "Thane Talley", DepartmentRefId = 3 },
                new Client { Id = 25, Name = "Cameron Dillon", DepartmentRefId = 3 },
                new Client { Id = 26, Name = "Baxter Macias", DepartmentRefId = 3 },
                new Client { Id = 27, Name = "Aphrodite Dixon", DepartmentRefId = 3 },
                new Client { Id = 28, Name = "Nicholas King", DepartmentRefId = 3 },
                new Client { Id = 29, Name = "Lydia Kirk", DepartmentRefId = 3 },
                new Client { Id = 30, Name = "Hop Buckley", DepartmentRefId = 3 }
            );

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, DepartmentName = DepartmentName.Individual, LoanRate = 15, DepositRate = 5 },
                new Department { Id = 2, DepartmentName = DepartmentName.Business, LoanRate = 10, DepositRate = 10 },
                new Department { Id = 3, DepartmentName = DepartmentName.Vip, LoanRate = 5, DepositRate = 15 }
            );

            modelBuilder.Entity<Money>().HasData(
                new Money { ClientId = 1, Funds = 13922, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 2, Funds = 8452, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 3, Funds = 20543, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 4, Funds = 40967, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 5, Funds = 4595, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 6, Funds = 25378, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 7, Funds = 17358, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 8, Funds = 41162, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 9, Funds = 10516, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 10, Funds = 10740, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 11, Funds = 26993, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 12, Funds = 1213, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 13, Funds = 21018, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 14, Funds = 5459, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 15, Funds = 37097, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 16, Funds = 15563, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 17, Funds = 12695, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 18, Funds = 18124, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 19, Funds = 9670, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 20, Funds = 45049, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 21, Funds = 36542, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 22, Funds = 21236, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 23, Funds = 41542, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 24, Funds = 29278, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 25, Funds = 1806, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 26, Funds = 4652, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 27, Funds = 7256, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 28, Funds = 11960, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 29, Funds = 31206, Loan = 0, Deposit = 0, DepositType = DepositType.No },
                new Money { ClientId = 30, Funds = 32768, Loan = 0, Deposit = 0, DepositType = DepositType.No }
            );

            modelBuilder
                .Entity<Department>()
                .Property(e => e.DepartmentName)
                .HasConversion<string>();

            modelBuilder
                .Entity<Money>()
                .Property(e => e.DepositType)
                .HasConversion<string>();
        }
    }
}
