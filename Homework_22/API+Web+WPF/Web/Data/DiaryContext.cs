using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Homework_22_Web.Models;

namespace Homework_22_Web.Data
{
    public class DiaryContext : IdentityDbContext<User>
    {
        public DbSet<Note> Notes { get; set; }

        public DiaryContext(DbContextOptions<DiaryContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>().HasData(
                    new Note { Id = 1, Name = "Clinton", Surname = "Nielsen", Phone = "073-950-032", Address = "548-9419 Ac St.", Iban = "MC4687627477700586662925905" },
                    new Note { Id = 2, Name = "Summer", Surname = "Horton", Phone = "981-871-867", Address = "P.O. Box 755, 5715 Velit Street", Iban = "DK0458275512361305" },
                    new Note { Id = 3, Name = "Brock", Surname = "Benson", Phone = "644-470-655", Address = "5539 Elit. St.", Iban = "IE50OCEY16214666156540" },
                    new Note { Id = 4, Name = "Donovan", Surname = "Sanchez", Phone = "021-627-378", Address = "Ap #972-9103 Eu Rd.", Iban = "SM7625407558845802833383352" },
                    new Note { Id = 5, Name = "Martena", Surname = "Stewart", Phone = "464-878-841", Address = "432-4010 Molestie Road", Iban = "AL04700264981286482617074842" },
                    new Note { Id = 6, Name = "Breanna", Surname = "Benson", Phone = "436-251-379", Address = "P.O. Box 351, 8135 Lorem Av.", Iban = "PL39719618279189562460931448" },
                    new Note { Id = 7, Name = "Octavia", Surname = "Cleveland", Phone = "494-387-366", Address = "448-9849 Blandit Ave", Iban = "GB98GWGL12119886433060" }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
