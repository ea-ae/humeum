﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230225033218_TaxSchemesSeedDataWorkaround")]
    partial class TaxSchemesSeedDataWorkaround
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("Domain.AssetAggregate.Asset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ProfileId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("ReturnRate")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("StandardDeviation")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("Assets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5430),
                            Description = "Index funds track the performance of a particular market index; great diversification, low fees, and easy management.",
                            ModifiedAt = new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5432),
                            Name = "Index fund (default)",
                            ReturnRate = 8.1m,
                            StandardDeviation = 15.2m
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5442),
                            Description = "Bond funds provide great diversification potential and are stereotypically less volatile than other securities.",
                            ModifiedAt = new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5442),
                            Name = "Bond fund (default)",
                            ReturnRate = 1.9m,
                            StandardDeviation = 3.0m
                        });
                });

            modelBuilder.Entity("Domain.ProfileAggregate.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("WithdrawalRate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("Domain.TaxSchemeAggregate.TaxScheme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TaxRate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TaxSchemes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5468),
                            Description = "Regular flat income tax in Estonia, applicable to all income by default. First 654EUR/mo aka 7848EUR/yr are tax-free.",
                            ModifiedAt = new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5469),
                            Name = "Income tax",
                            TaxRate = 20m
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5471),
                            Description = "Asset income invested through III pillar, with an account opened in 2021 or later. Term pensions based on life expectancy, not included here, provide a 20% discount.",
                            ModifiedAt = new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5471),
                            Name = "III pillar, post-2021",
                            TaxRate = 20m
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5472),
                            Description = "Asset income invested through III pillar, with an account opened before 2021. Term pensions based on life expectancy, not included here, provide a 20% discount.",
                            ModifiedAt = new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5472),
                            Name = "III pillar, pre-2021",
                            TaxRate = 20m
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5533),
                            Description = "Income that due to special circumstances (e.g. charity) is not taxed whatsoever.",
                            ModifiedAt = new DateTime(2023, 2, 25, 3, 32, 17, 861, DateTimeKind.Utc).AddTicks(5533),
                            Name = "Non-taxable income",
                            TaxRate = 0m
                        });
                });

            modelBuilder.Entity("Domain.TransactionAggregate.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<int?>("AssetId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProfileId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TaxSchemeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TypeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("ProfileId");

                    b.HasIndex("TaxSchemeId");

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
                            Code = "ALWAYS",
                            Name = "Always"
                        },
                        new
                        {
                            Id = 2,
                            Code = "PRERETIREMENTONLY",
                            Name = "Pre-retirement only"
                        },
                        new
                        {
                            Id = 3,
                            Code = "RETIREMENTONLY",
                            Name = "Retirement only"
                        });
                });

            modelBuilder.Entity("Infrastructure.Identity.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Domain.AssetAggregate.Asset", b =>
                {
                    b.HasOne("Domain.ProfileAggregate.Profile", "Profile")
                        .WithMany("Assets")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Domain.TaxSchemeAggregate.TaxScheme", b =>
                {
                    b.OwnsOne("Domain.TaxSchemeAggregate.ValueObjects.TaxIncentiveScheme", "IncentiveScheme", b1 =>
                        {
                            b1.Property<int>("TaxSchemeId")
                                .HasColumnType("INTEGER");

                            b1.Property<int?>("MaxApplicableIncome")
                                .HasColumnType("INTEGER");

                            b1.Property<decimal?>("MaxIncomePercentage")
                                .HasColumnType("TEXT");

                            b1.Property<int?>("MinAge")
                                .HasColumnType("INTEGER");

                            b1.Property<decimal>("TaxRefundRate")
                                .HasColumnType("TEXT");

                            b1.HasKey("TaxSchemeId");

                            b1.ToTable("TaxSchemes");

                            b1.WithOwner()
                                .HasForeignKey("TaxSchemeId");

                            b1.HasData(
                                new
                                {
                                    TaxSchemeId = 1,
                                    MaxApplicableIncome = 7848,
                                    TaxRefundRate = 20m
                                },
                                new
                                {
                                    TaxSchemeId = 2,
                                    MaxApplicableIncome = 6000,
                                    MaxIncomePercentage = 15m,
                                    MinAge = 60,
                                    TaxRefundRate = 10m
                                },
                                new
                                {
                                    TaxSchemeId = 3,
                                    MaxApplicableIncome = 6000,
                                    MaxIncomePercentage = 15m,
                                    MinAge = 55,
                                    TaxRefundRate = 10m
                                });
                        });

                    b.Navigation("IncentiveScheme");
                });

            modelBuilder.Entity("Domain.TransactionAggregate.Transaction", b =>
                {
                    b.HasOne("Domain.AssetAggregate.Asset", "Asset")
                        .WithMany("Transactions")
                        .HasForeignKey("AssetId");

                    b.HasOne("Domain.ProfileAggregate.Profile", "Profile")
                        .WithMany("Transactions")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.TaxSchemeAggregate.TaxScheme", "TaxScheme")
                        .WithMany("Transactions")
                        .HasForeignKey("TaxSchemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.TransactionAggregate.ValueObjects.TransactionType", "Type")
                        .WithMany()
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

                                    b2.Property<int>("TimeUnitId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<int>("TimesPerCycle")
                                        .HasColumnType("INTEGER");

                                    b2.Property<int>("UnitsInCycle")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("TimelineTransactionId");

                                    b2.HasIndex("TimeUnitId");

                                    b2.ToTable("Transactions");

                                    b2.HasOne("Domain.TransactionAggregate.ValueObjects.TimeUnit", "TimeUnit")
                                        .WithMany()
                                        .HasForeignKey("TimeUnitId")
                                        .OnDelete(DeleteBehavior.Cascade)
                                        .IsRequired();

                                    b2.WithOwner()
                                        .HasForeignKey("TimelineTransactionId");

                                    b2.Navigation("TimeUnit");
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

                    b.Navigation("Asset");

                    b.Navigation("PaymentTimeline")
                        .IsRequired();

                    b.Navigation("Profile");

                    b.Navigation("TaxScheme");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Infrastructure.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.AssetAggregate.Asset", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Domain.ProfileAggregate.Profile", b =>
                {
                    b.Navigation("Assets");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Domain.TaxSchemeAggregate.TaxScheme", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
