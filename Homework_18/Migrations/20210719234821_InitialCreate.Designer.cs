﻿// <auto-generated />
using Homework_18;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Homework_18.Migrations
{
    [DbContext(typeof(AppContext))]
    [Migration("20210719234821_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Homework_18.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DepartmentRefId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentRefId");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DepartmentRefId = 1,
                            Name = "Orson Avery"
                        },
                        new
                        {
                            Id = 2,
                            DepartmentRefId = 1,
                            Name = "Whoopi Franks"
                        },
                        new
                        {
                            Id = 3,
                            DepartmentRefId = 1,
                            Name = "Kermit Olsen"
                        },
                        new
                        {
                            Id = 4,
                            DepartmentRefId = 1,
                            Name = "Yoshi Gallagher"
                        },
                        new
                        {
                            Id = 5,
                            DepartmentRefId = 1,
                            Name = "Hamish Cole"
                        },
                        new
                        {
                            Id = 6,
                            DepartmentRefId = 1,
                            Name = "Emma Sharp"
                        },
                        new
                        {
                            Id = 7,
                            DepartmentRefId = 1,
                            Name = "Hilary Coleman"
                        },
                        new
                        {
                            Id = 8,
                            DepartmentRefId = 1,
                            Name = "Vance Barlow"
                        },
                        new
                        {
                            Id = 9,
                            DepartmentRefId = 1,
                            Name = "Felicia Sutton"
                        },
                        new
                        {
                            Id = 10,
                            DepartmentRefId = 1,
                            Name = "Dexter Huber"
                        },
                        new
                        {
                            Id = 11,
                            DepartmentRefId = 2,
                            Name = "Wynne Gilliam"
                        },
                        new
                        {
                            Id = 12,
                            DepartmentRefId = 2,
                            Name = "Austin Wilkins"
                        },
                        new
                        {
                            Id = 13,
                            DepartmentRefId = 2,
                            Name = "Tiger Whitehead"
                        },
                        new
                        {
                            Id = 14,
                            DepartmentRefId = 2,
                            Name = "Ora Weaver"
                        },
                        new
                        {
                            Id = 15,
                            DepartmentRefId = 2,
                            Name = "Ray Lyons"
                        },
                        new
                        {
                            Id = 16,
                            DepartmentRefId = 2,
                            Name = "Alden Ingram"
                        },
                        new
                        {
                            Id = 17,
                            DepartmentRefId = 2,
                            Name = "Gabriel Perez"
                        },
                        new
                        {
                            Id = 18,
                            DepartmentRefId = 2,
                            Name = "Xanthus Knapp"
                        },
                        new
                        {
                            Id = 19,
                            DepartmentRefId = 2,
                            Name = "Juliet Clark"
                        },
                        new
                        {
                            Id = 20,
                            DepartmentRefId = 2,
                            Name = "Oscar Coleman"
                        },
                        new
                        {
                            Id = 21,
                            DepartmentRefId = 3,
                            Name = "Myles Marsh"
                        },
                        new
                        {
                            Id = 22,
                            DepartmentRefId = 3,
                            Name = "Trevor Mercado"
                        },
                        new
                        {
                            Id = 23,
                            DepartmentRefId = 3,
                            Name = "Leo Little"
                        },
                        new
                        {
                            Id = 24,
                            DepartmentRefId = 3,
                            Name = "Thane Talley"
                        },
                        new
                        {
                            Id = 25,
                            DepartmentRefId = 3,
                            Name = "Cameron Dillon"
                        },
                        new
                        {
                            Id = 26,
                            DepartmentRefId = 3,
                            Name = "Baxter Macias"
                        },
                        new
                        {
                            Id = 27,
                            DepartmentRefId = 3,
                            Name = "Aphrodite Dixon"
                        },
                        new
                        {
                            Id = 28,
                            DepartmentRefId = 3,
                            Name = "Nicholas King"
                        },
                        new
                        {
                            Id = 29,
                            DepartmentRefId = 3,
                            Name = "Lydia Kirk"
                        },
                        new
                        {
                            Id = 30,
                            DepartmentRefId = 3,
                            Name = "Hop Buckley"
                        });
                });

            modelBuilder.Entity("Homework_18.Entities.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DepositRate")
                        .HasColumnType("int");

                    b.Property<int>("LoanRate")
                        .HasColumnType("int");

                    b.Property<int>("Name")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DepositRate = 5,
                            LoanRate = 15,
                            Name = 0
                        },
                        new
                        {
                            Id = 2,
                            DepositRate = 10,
                            LoanRate = 10,
                            Name = 1
                        },
                        new
                        {
                            Id = 3,
                            DepositRate = 15,
                            LoanRate = 5,
                            Name = 2
                        });
                });

            modelBuilder.Entity("Homework_18.Entities.Money", b =>
                {
                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<decimal>("Deposit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Funds")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Loan")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("ClientId");

                    b.ToTable("Money");

                    b.HasData(
                        new
                        {
                            ClientId = 1,
                            Deposit = 0m,
                            Funds = 13922m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 2,
                            Deposit = 0m,
                            Funds = 8452m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 3,
                            Deposit = 0m,
                            Funds = 20543m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 4,
                            Deposit = 0m,
                            Funds = 40967m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 5,
                            Deposit = 0m,
                            Funds = 4595m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 6,
                            Deposit = 0m,
                            Funds = 25378m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 7,
                            Deposit = 0m,
                            Funds = 17358m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 8,
                            Deposit = 0m,
                            Funds = 41162m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 9,
                            Deposit = 0m,
                            Funds = 10516m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 10,
                            Deposit = 0m,
                            Funds = 10740m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 11,
                            Deposit = 0m,
                            Funds = 26993m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 12,
                            Deposit = 0m,
                            Funds = 1213m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 13,
                            Deposit = 0m,
                            Funds = 21018m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 14,
                            Deposit = 0m,
                            Funds = 5459m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 15,
                            Deposit = 0m,
                            Funds = 37097m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 16,
                            Deposit = 0m,
                            Funds = 15563m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 17,
                            Deposit = 0m,
                            Funds = 12695m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 18,
                            Deposit = 0m,
                            Funds = 18124m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 19,
                            Deposit = 0m,
                            Funds = 9670m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 20,
                            Deposit = 0m,
                            Funds = 45049m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 21,
                            Deposit = 0m,
                            Funds = 36542m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 22,
                            Deposit = 0m,
                            Funds = 21236m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 23,
                            Deposit = 0m,
                            Funds = 41542m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 24,
                            Deposit = 0m,
                            Funds = 29278m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 25,
                            Deposit = 0m,
                            Funds = 1806m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 26,
                            Deposit = 0m,
                            Funds = 4652m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 27,
                            Deposit = 0m,
                            Funds = 7256m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 28,
                            Deposit = 0m,
                            Funds = 11960m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 29,
                            Deposit = 0m,
                            Funds = 31206m,
                            Loan = 0m,
                            Type = 0
                        },
                        new
                        {
                            ClientId = 30,
                            Deposit = 0m,
                            Funds = 32768m,
                            Loan = 0m,
                            Type = 0
                        });
                });

            modelBuilder.Entity("Homework_18.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientRefId")
                        .HasColumnType("int");

                    b.Property<string>("Operation")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("ClientRefId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Homework_18.Entities.Client", b =>
                {
                    b.HasOne("Homework_18.Entities.Department", "Department")
                        .WithMany("Clients")
                        .HasForeignKey("DepartmentRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Homework_18.Entities.Money", b =>
                {
                    b.HasOne("Homework_18.Entities.Client", "Client")
                        .WithOne("Funds")
                        .HasForeignKey("Homework_18.Entities.Money", "ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Homework_18.Entities.Transaction", b =>
                {
                    b.HasOne("Homework_18.Entities.Client", "Client")
                        .WithMany("Transactions")
                        .HasForeignKey("ClientRefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Homework_18.Entities.Client", b =>
                {
                    b.Navigation("Funds");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Homework_18.Entities.Department", b =>
                {
                    b.Navigation("Clients");
                });
#pragma warning restore 612, 618
        }
    }
}
