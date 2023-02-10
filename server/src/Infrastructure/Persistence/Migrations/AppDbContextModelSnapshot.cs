﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("Domain.TransactionAggregate.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("TimeUnitId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TypeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TimeUnitId");

                    b.HasIndex("TypeId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Domain.TransactionAggregate.ValueObjects.TimeUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("TransactionTimeUnits");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Code = "DAYS",
                            Name = "Days"
                        },
                        new
                        {
                            Id = 3,
                            Code = "WEEKS",
                            Name = "Weeks"
                        },
                        new
                        {
                            Id = 4,
                            Code = "MONTHS",
                            Name = "Months"
                        },
                        new
                        {
                            Id = 5,
                            Code = "YEARS",
                            Name = "Years"
                        });
                });

            modelBuilder.Entity("Domain.TransactionAggregate.ValueObjects.TransactionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("TransactionTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "INCOME",
                            Name = "Income"
                        },
                        new
                        {
                            Id = 2,
                            Code = "EXPENSE",
                            Name = "Expense"
                        });
                });

            modelBuilder.Entity("Domain.TransactionAggregate.Transaction", b =>
                {
                    b.HasOne("Domain.TransactionAggregate.ValueObjects.TimeUnit", null)
                        .WithMany("Transactions")
                        .HasForeignKey("TimeUnitId");

                    b.HasOne("Domain.TransactionAggregate.ValueObjects.TransactionType", "Type")
                        .WithMany("Transactions")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.TransactionAggregate.ValueObjects.Timeline", "PaymentTimeline", b1 =>
                        {
                            b1.Property<int>("TransactionId")
                                .HasColumnType("INTEGER");

                            b1.HasKey("TransactionId");

                            b1.ToTable("Transactions");

                            b1.WithOwner()
                                .HasForeignKey("TransactionId");

                            b1.OwnsOne("Domain.TransactionAggregate.ValueObjects.Frequency", "Frequency", b2 =>
                                {
                                    b2.Property<int>("TimelineTransactionId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<int>("TimesPerUnit")
                                        .HasColumnType("INTEGER");

                                    b2.Property<int>("UnitId")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("TimelineTransactionId");

                                    b2.HasIndex("UnitId");

                                    b2.ToTable("Transactions");

                                    b2.WithOwner()
                                        .HasForeignKey("TimelineTransactionId");

                                    b2.HasOne("Domain.TransactionAggregate.ValueObjects.TimeUnit", "Unit")
                                        .WithMany()
                                        .HasForeignKey("UnitId")
                                        .OnDelete(DeleteBehavior.Cascade)
                                        .IsRequired();

                                    b2.Navigation("Unit");
                                });

                            b1.OwnsOne("Domain.TransactionAggregate.ValueObjects.TimePeriod", "Period", b2 =>
                                {
                                    b2.Property<int>("TimelineTransactionId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<DateOnly?>("End")
                                        .HasColumnType("TEXT");

                                    b2.Property<DateOnly>("Start")
                                        .HasColumnType("TEXT");

                                    b2.HasKey("TimelineTransactionId");

                                    b2.ToTable("Transactions");

                                    b2.WithOwner()
                                        .HasForeignKey("TimelineTransactionId");
                                });

                            b1.Navigation("Frequency");

                            b1.Navigation("Period")
                                .IsRequired();
                        });

                    b.Navigation("PaymentTimeline")
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Domain.TransactionAggregate.ValueObjects.TimeUnit", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Domain.TransactionAggregate.ValueObjects.TransactionType", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
