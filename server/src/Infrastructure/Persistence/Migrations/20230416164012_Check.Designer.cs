﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230416164012_Check")]
    partial class Check
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.AssetAggregate.Asset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ProfileId")
                        .HasColumnType("integer");

                    b.Property<decimal>("ReturnRate")
                        .HasColumnType("numeric");

                    b.Property<decimal>("StandardDeviation")
                        .HasColumnType("numeric");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.HasIndex("TypeId");

                    b.ToTable("Assets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 4, 16, 16, 40, 12, 290, DateTimeKind.Utc).AddTicks(1555),
                            Description = "Index funds track the performance of a particular market index; great diversification, low fees, and easy management.",
                            ModifiedAt = new DateTime(2023, 4, 16, 16, 40, 12, 290, DateTimeKind.Utc).AddTicks(1557),
                            Name = "Index fund (default)",
                            ReturnRate = 8.1m,
                            StandardDeviation = 15.2m,
                            TypeId = 2
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2023, 4, 16, 16, 40, 12, 290, DateTimeKind.Utc).AddTicks(1670),
                            Description = "Bond funds provide great diversification potential and are stereotypically less volatile than other securities.",
                            ModifiedAt = new DateTime(2023, 4, 16, 16, 40, 12, 290, DateTimeKind.Utc).AddTicks(1670),
                            Name = "Bond fund (default)",
                            ReturnRate = 1.9m,
                            StandardDeviation = 3.0m,
                            TypeId = 5
                        });
                });

            modelBuilder.Entity("Domain.ProfileAggregate.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<decimal>("WithdrawalRate")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("Domain.TaxSchemeAggregate.TaxScheme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("TaxRate")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("TaxSchemes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 4, 16, 16, 40, 12, 290, DateTimeKind.Utc).AddTicks(1740),
                            Description = "Regular flat income tax in Estonia, applicable to all income by default. First 654EUR/mo aka 7848EUR/yr are tax-free.",
                            ModifiedAt = new DateTime(2023, 4, 16, 16, 40, 12, 290, DateTimeKind.Utc).AddTicks(1740),
                            Name = "Income tax",
                            TaxRate = 20m
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2023, 4, 16, 16, 40, 12, 290, DateTimeKind.Utc).AddTicks(1742),
                            Description = "Asset income invested through III pillar, with an account opened in 2021 or later. Term pensions based on life expectancy, not included here, provide a 20% discount.",
                            ModifiedAt = new DateTime(2023, 4, 16, 16, 40, 12, 290, DateTimeKind.Utc).AddTicks(1743),
                            Name = "III pillar, post-2021",
                            TaxRate = 20m
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2023, 4, 16, 16, 40, 12, 290, DateTimeKind.Utc).AddTicks(1744),
                            Description = "Asset income invested through III pillar, with an account opened before 2021. Term pensions based on life expectancy, not included here, provide a 20% discount.",
                            ModifiedAt = new DateTime(2023, 4, 16, 16, 40, 12, 290, DateTimeKind.Utc).AddTicks(1744),
                            Name = "III pillar, pre-2021",
                            TaxRate = 20m
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2023, 4, 16, 16, 40, 12, 290, DateTimeKind.Utc).AddTicks(1745),
                            Description = "Income that due to special circumstances (e.g. charity) is not taxed whatsoever.",
                            ModifiedAt = new DateTime(2023, 4, 16, 16, 40, 12, 290, DateTimeKind.Utc).AddTicks(1745),
                            Name = "Non-taxable income",
                            TaxRate = 0m
                        });
                });

            modelBuilder.Entity("Domain.TransactionAggregate.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int?>("AssetId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("ProfileId")
                        .HasColumnType("integer");

                    b.Property<int>("TaxSchemeId")
                        .HasColumnType("integer");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("ProfileId");

                    b.HasIndex("TaxSchemeId");

                    b.HasIndex("TypeId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Domain.TransactionAggregate.ValueObjects.AssetType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AssetTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "LIQUID",
                            Name = "Liquid/Cash"
                        },
                        new
                        {
                            Id = 2,
                            Code = "INDEX",
                            Name = "Index fund"
                        },
                        new
                        {
                            Id = 3,
                            Code = "MANAGED",
                            Name = "Managed fund"
                        },
                        new
                        {
                            Id = 4,
                            Code = "REALESTATE",
                            Name = "Real estate"
                        },
                        new
                        {
                            Id = 5,
                            Code = "BOND",
                            Name = "Bond"
                        },
                        new
                        {
                            Id = 6,
                            Code = "STOCK",
                            Name = "Stock/Derivative"
                        },
                        new
                        {
                            Id = 7,
                            Code = "OTHER",
                            Name = "Other"
                        });
                });

            modelBuilder.Entity("Domain.TransactionAggregate.ValueObjects.TimeUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

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
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

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

            modelBuilder.Entity("Domain.TransactionCategoryAggregate.TransactionCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ProfileId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("TransactionCategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4453),
                            ModifiedAt = new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4456),
                            Name = "General"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4459),
                            ModifiedAt = new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4459),
                            Name = "Investing"
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4460),
                            ModifiedAt = new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4460),
                            Name = "Work, Education, & Business"
                        },
                        new
                        {
                            Id = 4,
                            CreatedAt = new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4461),
                            ModifiedAt = new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4462),
                            Name = "Recreation & Lifestyle"
                        },
                        new
                        {
                            Id = 5,
                            CreatedAt = new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4463),
                            ModifiedAt = new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4463),
                            Name = "Food & Clothing"
                        },
                        new
                        {
                            Id = 6,
                            CreatedAt = new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4464),
                            ModifiedAt = new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4465),
                            Name = "Housing & Utilities"
                        },
                        new
                        {
                            Id = 7,
                            CreatedAt = new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4514),
                            ModifiedAt = new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4515),
                            Name = "Transportation"
                        },
                        new
                        {
                            Id = 8,
                            CreatedAt = new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4516),
                            ModifiedAt = new DateTime(2023, 4, 16, 16, 40, 12, 289, DateTimeKind.Utc).AddTicks(4516),
                            Name = "Gifts & Donations"
                        });
                });

            modelBuilder.Entity("Infrastructure.Auth.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("Enabled")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("RefreshTokenExpiry")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

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
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

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
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("TransactionTransactionCategory", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("integer");

                    b.Property<int>("TransactionsId")
                        .HasColumnType("integer");

                    b.HasKey("CategoriesId", "TransactionsId");

                    b.HasIndex("TransactionsId");

                    b.ToTable("TransactionWithCategory", (string)null);
                });

            modelBuilder.Entity("Domain.AssetAggregate.Asset", b =>
                {
                    b.HasOne("Domain.ProfileAggregate.Profile", "Profile")
                        .WithMany("Assets")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.TransactionAggregate.ValueObjects.AssetType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Domain.TaxSchemeAggregate.TaxScheme", b =>
                {
                    b.OwnsOne("Domain.TaxSchemeAggregate.ValueObjects.TaxIncentiveScheme", "IncentiveScheme", b1 =>
                        {
                            b1.Property<int>("TaxSchemeId")
                                .HasColumnType("integer");

                            b1.Property<int?>("MaxApplicableIncome")
                                .HasColumnType("integer");

                            b1.Property<decimal?>("MaxIncomePercentage")
                                .HasColumnType("numeric");

                            b1.Property<int?>("MinAge")
                                .HasColumnType("integer");

                            b1.Property<decimal>("TaxRefundRate")
                                .HasColumnType("numeric");

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
                                .HasColumnType("integer");

                            b1.HasKey("TransactionId");

                            b1.ToTable("Transactions");

                            b1.WithOwner()
                                .HasForeignKey("TransactionId");

                            b1.OwnsOne("Domain.TransactionAggregate.ValueObjects.Frequency", "Frequency", b2 =>
                                {
                                    b2.Property<int>("TimelineTransactionId")
                                        .HasColumnType("integer");

                                    b2.Property<int>("TimeUnitId")
                                        .HasColumnType("integer");

                                    b2.Property<int>("TimesPerCycle")
                                        .HasColumnType("integer");

                                    b2.Property<int>("UnitsInCycle")
                                        .HasColumnType("integer");

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
                                        .HasColumnType("integer");

                                    b2.Property<DateOnly?>("End")
                                        .HasColumnType("date");

                                    b2.Property<DateOnly>("Start")
                                        .HasColumnType("date");

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

            modelBuilder.Entity("Domain.TransactionCategoryAggregate.TransactionCategory", b =>
                {
                    b.HasOne("Domain.ProfileAggregate.Profile", "Profile")
                        .WithMany("TransactionCategories")
                        .HasForeignKey("ProfileId");

                    b.Navigation("Profile");
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
                    b.HasOne("Infrastructure.Auth.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Infrastructure.Auth.ApplicationUser", null)
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

                    b.HasOne("Infrastructure.Auth.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Infrastructure.Auth.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TransactionTransactionCategory", b =>
                {
                    b.HasOne("Domain.TransactionCategoryAggregate.TransactionCategory", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.TransactionAggregate.Transaction", null)
                        .WithMany()
                        .HasForeignKey("TransactionsId")
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

                    b.Navigation("TransactionCategories");

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
