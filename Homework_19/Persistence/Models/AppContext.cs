using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Models
{
    public class AppContext : DbContext, IDbContext
    {
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;
        public DbSet<Money> Money { get; set; } = null!;

        public AppContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(("Server=(localdb)\\mssqllocaldb;Database=bank2;Trusted_Connection=True;"), 
                x => x.MigrationsAssembly("Persistence"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Client>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder
                .Entity<Client>()
                .Property(p => p.Name)
                .IsRequired();

            modelBuilder
                .Entity<Client>()
                .HasOne(c => c.Funds)
                .WithOne(m => m.Client)
                .HasForeignKey<Money>(m => m.ClientId);

            modelBuilder
                .Entity<Client>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Clients)
                .HasForeignKey(c => c.DepartmentRefId);

            modelBuilder
                .Entity<Department>()
                .Property(p => p.DepartmentName)
                .HasConversion<string>();

            modelBuilder
                .Entity<Department>()
                .Ignore(p => p.DepartmentName);

            modelBuilder
                .Entity<Department>()
                .Property(d => d.DepartmentNameString)
                .HasColumnName("DepartmentName");

            modelBuilder
                .Entity<Transaction>()
                .HasOne(t => t.Client)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.ClientRefId);

            modelBuilder
                .Entity<Transaction>()
                .Property(p => p.Operation)
                .IsRequired();

            modelBuilder
                .Entity<Transaction>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder
                .Entity<Money>()
                .HasKey(k => k.ClientId);

            modelBuilder
                .Entity<Money>()
                .Property(p => p.DepositType)
                .HasConversion<string>();

            modelBuilder
                .Entity<Money>()
                .Ignore(p => p.DepositType);

            modelBuilder
                .Entity<Money>()
                .Property(m => m.DepositTypeString)
                .HasColumnName("DepositType");

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
        }

        //------------------------------------------------------------------

        public List<Department> DepartmentsList()
        {
            List<Department> dep = new();

            using (AppContext context = new())
            {
                List<Department> depList = context.Departments.ToList();

                foreach (var item in depList)
                {
                    dep.Add(item);
                }
            }

            return dep;
        }

        public Dictionary<string, decimal> ShowClients(int depId)
        {
            Dictionary<string, decimal> clientsList = new();

            using (AppContext context = new())
            {
                var clients = from client in context.Clients
                    join department in context.Departments on client.DepartmentRefId equals department.Id
                    join money in context.Money on client.Id equals money.ClientId
                    where department.Id == depId
                    select new
                    {
                        Name = client.Name,
                        Funds = money.Funds
                    };

                foreach (var client in clients)
                {
                    clientsList.Add(client.Name, client.Funds);
                }
            }
            return clientsList;
        }

        public decimal GetClientDeposit(int clientId)
        {
            decimal deposit;

            using (AppContext context = new())
            {
                var result = context.Clients.Join(context.Money,
                    c => c.Id,
                    m => m.ClientId,
                    (c, m) => new
                    {
                        Id = m.ClientId,
                        Deposit = m.Deposit,
                    });
                var client = result.Single(m => m.Id == clientId);

                deposit = client.Deposit;
            }

            return deposit;
        }

        public string GetClientDepositType(int clientId)
        {
            string depositType;

            using (AppContext context = new())
            {
                var result = context.Clients.Join(context.Money,
                    c => c.Id,
                    m => m.ClientId,
                    (c, m) => new
                    {
                        Id = m.ClientId,
                        DepositType = m.DepositTypeString
                    });
                var client = result.Single(m => m.Id == clientId);

                depositType = client.DepositType;
            }

            return depositType;
        }

        public decimal GetClientFunds(int clientId)
        {
            decimal funds;

            using (AppContext context = new())
            {
                var result = context.Clients.Join(context.Money,
                    c => c.Id,
                    m => m.ClientId,
                    (c, m) => new
                    {
                        Id = m.ClientId,
                        Funds = m.Funds,
                    });
                var client = result.Single(m => m.Id == clientId);

                funds = client.Funds;
            }

            return funds;
        }

        public int GetClientId(string name)
        {
            int id;

            using (AppContext context = new())
            {
                var client = context.Clients.First(c => c.Name == name);
                id = client.Id;
            }
            return id;
        }

        public decimal GetClientLoan(int clientId)
        {
            decimal loan;

            using (AppContext context = new())
            {
                var result = context.Clients.Join(context.Money,
                    c => c.Id,
                    m => m.ClientId,
                    (c, m) => new
                    {
                        Id = m.ClientId,
                        Loan = m.Loan,
                    });
                var client = result.Single(m => m.Id == clientId);

                loan = client.Loan;
            }

            return loan;
        }

        public string GetClientName(int clientId)
        {
            string name;

            using (AppContext context = new())
            {
                var client = context.Clients.Single(c => c.Id == clientId);
                name = client.Name;
            }
            return name;
        }

        public int GetDepartmentId(string depName)
        {
            int depId;

            using (AppContext context = new())
            {
                var dep = context.Departments.Single(d => d.DepartmentNameString == depName);
                depId = dep.Id;
            }
            return depId;
        }

        public int GetDepartmentLoanRate(int depId)
        {
            int depLoanRate;

            using (AppContext context = new())
            {
                var result = context.Clients.Join(context.Departments,
                    c => c.Id,
                    d => d.Id,
                    (c, d) => new
                    {
                        Id = d.Id,
                        LoanRate = d.LoanRate,
                    });
                var dep = result.Single(d => d.Id == depId);

                depLoanRate = dep.LoanRate;
            }

            return depLoanRate;
        }

        public int GetDepartmentDepositRate(int depId)
        {
            int depDepositRate;

            using (AppContext context = new())
            {
                var result = context.Clients.Join(context.Departments,
                    c => c.Id,
                    d => d.Id,
                    (c, d) => new
                    {
                        Id = d.Id,
                        DepositRate = d.DepositRate
                    });
                var dep = result.Single(d => d.Id == depId);

                depDepositRate = dep.DepositRate;
            }

            return depDepositRate;
        }

        public void GetLoan(int clientId, decimal amount)
        {
            throw new NotImplementedException();
        }

        public decimal GetDepositAmount(int clientId)
        {
            decimal money;

            using (AppContext context = new())
            {
                var client = context.Money.First(c => c.ClientId == clientId);
                money = client.Deposit;
            }
            return money;
        }

        public decimal GetLoanAmount(int clientId)
        {
            decimal money;

            using (AppContext context = new())
            {
                var client = context.Money.First(c => c.ClientId == clientId);
                money = client.Loan;
            }
            return money;
        }

        public decimal GetFundsAmount(int clientId)
        {
            decimal money;

            using (AppContext context = new())
            {
                var client = context.Money.First(c => c.ClientId == clientId);
                money = client.Funds;
            }
            return money;
        }

        public void SetDepositAsCapitalized(int clientId)
        {
            using (AppContext context = new())
            {
                var client = context.Money.FirstOrDefault(c => c.ClientId == clientId);

                if (client != null)
                {
                    client.DepositType = DepositType.Capitalization;
                    _ = context.SaveChanges();
                }
            }
        }

        public void SetDepositAsSimple(int clientId)
        {
            using (AppContext context = new())
            {
                var client = context.Money.FirstOrDefault(c => c.ClientId == clientId);

                if (client != null)
                {
                    client.DepositType = DepositType.Simple;
                    _ = context.SaveChanges();
                }
            }
        }

        public void UpdateLoanAmount(int clientId, decimal amount)
        {
            using (AppContext context = new())
            {
                var client = context.Money.FirstOrDefault(c => c.ClientId == clientId);

                if (client != null)
                {
                    client.Loan = amount;
                    _ = context.SaveChanges();
                }
            }
        }

        public void UpdateDepositAmount(int clientId, decimal amount)
        {
            using (AppContext context = new())
            {
                var client = context.Money.FirstOrDefault(c => c.ClientId == clientId);

                if (client != null)
                {
                    client.Deposit = amount;
                    _ = context.SaveChanges();
                }
            }
        }

        public void UpdateFundsAmount(int clientId, decimal amount)
        {
            using (AppContext context = new())
            {
                var client = context.Money.FirstOrDefault(c => c.ClientId == clientId);

                if (client != null)
                {
                    client.Funds = amount;
                    _ = context.SaveChanges();
                }
            }
        }

        public void AddTransaction(int clientId, string operation)
        {
            using (AppContext context = new())
            {
                Transaction transaction = new()
                {
                    Operation = operation,
                    ClientRefId = clientId
                };

                _ = context.Transactions.Add(transaction);
                _ = context.SaveChanges();
            }
        }
    }
}
