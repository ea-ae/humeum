using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DisplayName = table.Column<string>(type: "text", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiry = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    RetirementGoal = table.Column<decimal>(type: "numeric", nullable: false),
                    WithdrawalRate = table.Column<decimal>(type: "numeric", nullable: false),
                    Birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxSchemes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TaxRate = table.Column<decimal>(type: "numeric", nullable: false),
                    IncentiveSchemeTaxRefundRate = table.Column<decimal>(name: "IncentiveScheme_TaxRefundRate", type: "numeric", nullable: true),
                    IncentiveSchemeMinAge = table.Column<int>(name: "IncentiveScheme_MinAge", type: "integer", nullable: true),
                    IncentiveSchemeMaxIncomePercentage = table.Column<decimal>(name: "IncentiveScheme_MaxIncomePercentage", type: "numeric", nullable: true),
                    IncentiveSchemeMaxApplicableIncome = table.Column<int>(name: "IncentiveScheme_MaxApplicableIncome", type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxSchemes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTimeUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTimeUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ReturnRate = table.Column<decimal>(type: "numeric", nullable: false),
                    StandardDeviation = table.Column<decimal>(type: "numeric", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    ProfileId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_AssetTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "AssetTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assets_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ProfileId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionCategories_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentTimelinePeriodStart = table.Column<DateOnly>(name: "PaymentTimeline_Period_Start", type: "date", nullable: false),
                    PaymentTimelinePeriodEnd = table.Column<DateOnly>(name: "PaymentTimeline_Period_End", type: "date", nullable: true),
                    PaymentTimelineFrequencyTimesPerCycle = table.Column<int>(name: "PaymentTimeline_Frequency_TimesPerCycle", type: "integer", nullable: true),
                    PaymentTimelineFrequencyUnitsInCycle = table.Column<int>(name: "PaymentTimeline_Frequency_UnitsInCycle", type: "integer", nullable: true),
                    PaymentTimelineFrequencyTimeUnitId = table.Column<int>(name: "PaymentTimeline_Frequency_TimeUnitId", type: "integer", nullable: true),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    ProfileId = table.Column<int>(type: "integer", nullable: false),
                    TaxSchemeId = table.Column<int>(type: "integer", nullable: false),
                    AssetId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_TaxSchemes_TaxSchemeId",
                        column: x => x.TaxSchemeId,
                        principalTable: "TaxSchemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionTimeUnits_PaymentTimeline_Frequency~",
                        column: x => x.PaymentTimelineFrequencyTimeUnitId,
                        principalTable: "TransactionTimeUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TransactionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionWithCategory",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "integer", nullable: false),
                    TransactionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionWithCategory", x => new { x.CategoriesId, x.TransactionsId });
                    table.ForeignKey(
                        name: "FK_TransactionWithCategory_TransactionCategories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "TransactionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionWithCategory_Transactions_TransactionsId",
                        column: x => x.TransactionsId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AssetTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "LIQUID", "Liquid/Cash" },
                    { 2, "INDEX", "Index fund" },
                    { 3, "MANAGED", "Managed fund" },
                    { 4, "REALESTATE", "Real estate" },
                    { 5, "BOND", "Bond" },
                    { 6, "STOCK", "Stock/Derivative" },
                    { 7, "OTHER", "Other" }
                });

            migrationBuilder.InsertData(
                table: "TaxSchemes",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "ModifiedAt", "Name", "TaxRate", "IncentiveScheme_MaxApplicableIncome", "IncentiveScheme_MaxIncomePercentage", "IncentiveScheme_MinAge", "IncentiveScheme_TaxRefundRate" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 25, 10, 2, 9, 440, DateTimeKind.Utc).AddTicks(566), null, "Regular flat income tax in Estonia, applicable to all income by default. First 654EUR/mo aka 7848EUR/yr are tax-free.", new DateTime(2023, 5, 25, 10, 2, 9, 440, DateTimeKind.Utc).AddTicks(570), "Income tax", 20m, 7848, null, null, 20m },
                    { 2, new DateTime(2023, 5, 25, 10, 2, 9, 441, DateTimeKind.Utc).AddTicks(1190), null, "Asset income invested through III pillar, with an account opened in 2021 or later. Term pensions based on life expectancy, not included here, provide a 20% discount.", new DateTime(2023, 5, 25, 10, 2, 9, 441, DateTimeKind.Utc).AddTicks(1195), "III pillar, post-2021", 20m, 6000, 15m, 60, 10m },
                    { 3, new DateTime(2023, 5, 25, 10, 2, 9, 441, DateTimeKind.Utc).AddTicks(1224), null, "Asset income invested through III pillar, with an account opened before 2021. Term pensions based on life expectancy, not included here, provide a 20% discount.", new DateTime(2023, 5, 25, 10, 2, 9, 441, DateTimeKind.Utc).AddTicks(1224), "III pillar, pre-2021", 20m, 6000, 15m, 55, 10m }
                });

            migrationBuilder.InsertData(
                table: "TaxSchemes",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "ModifiedAt", "Name", "TaxRate" },
                values: new object[] { 4, new DateTime(2023, 5, 25, 10, 2, 9, 441, DateTimeKind.Utc).AddTicks(1229), null, "Income that due to special circumstances (e.g. charity) is not taxed whatsoever.", new DateTime(2023, 5, 25, 10, 2, 9, 441, DateTimeKind.Utc).AddTicks(1230), "Non-taxable income", 0m });

            migrationBuilder.InsertData(
                table: "TransactionCategories",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "ModifiedAt", "Name", "ProfileId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4416), null, new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4417), "General", null },
                    { 2, new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4419), null, new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4419), "Investing", null },
                    { 3, new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4420), null, new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4420), "Work, Education, & Business", null },
                    { 4, new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4421), null, new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4422), "Recreation & Lifestyle", null },
                    { 5, new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4422), null, new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4423), "Food & Clothing", null },
                    { 6, new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4423), null, new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4424), "Housing & Utilities", null },
                    { 7, new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4425), null, new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4425), "Transportation", null },
                    { 8, new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4426), null, new DateTime(2023, 5, 25, 10, 2, 9, 890, DateTimeKind.Utc).AddTicks(4426), "Gifts & Donations", null }
                });

            migrationBuilder.InsertData(
                table: "TransactionTimeUnits",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 2, "DAYS", "Days" },
                    { 3, "WEEKS", "Weeks" },
                    { 4, "MONTHS", "Months" },
                    { 5, "YEARS", "Years" }
                });

            migrationBuilder.InsertData(
                table: "TransactionTypes",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "ALWAYS", "Always" },
                    { 2, "PRERETIREMENTONLY", "Pre-retirement only" },
                    { 3, "RETIREMENTONLY", "Retirement only" }
                });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "ModifiedAt", "Name", "ProfileId", "ReturnRate", "StandardDeviation", "TypeId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 5, 25, 10, 2, 9, 891, DateTimeKind.Utc).AddTicks(2255), null, "Index funds track the performance of a particular market index; great diversification, low fees, and easy management.", new DateTime(2023, 5, 25, 10, 2, 9, 891, DateTimeKind.Utc).AddTicks(2257), "Index fund (default)", null, 8.1m, 15.2m, 2 },
                    { 2, new DateTime(2023, 5, 25, 10, 2, 9, 891, DateTimeKind.Utc).AddTicks(2308), null, "Bond funds provide great diversification potential and are stereotypically less volatile than other securities.", new DateTime(2023, 5, 25, 10, 2, 9, 891, DateTimeKind.Utc).AddTicks(2309), "Bond fund (default)", null, 1.9m, 3.0m, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assets_ProfileId",
                table: "Assets",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_TypeId",
                table: "Assets",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCategories_ProfileId",
                table: "TransactionCategories",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AssetId",
                table: "Transactions",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PaymentTimeline_Frequency_TimeUnitId",
                table: "Transactions",
                column: "PaymentTimeline_Frequency_TimeUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ProfileId",
                table: "Transactions",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TaxSchemeId",
                table: "Transactions",
                column: "TaxSchemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TypeId",
                table: "Transactions",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTimeUnits_Code",
                table: "TransactionTimeUnits",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTypes_Code",
                table: "TransactionTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionWithCategory_TransactionsId",
                table: "TransactionWithCategory",
                column: "TransactionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "TransactionWithCategory");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TransactionCategories");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "TaxSchemes");

            migrationBuilder.DropTable(
                name: "TransactionTimeUnits");

            migrationBuilder.DropTable(
                name: "TransactionTypes");

            migrationBuilder.DropTable(
                name: "AssetTypes");

            migrationBuilder.DropTable(
                name: "Profiles");
        }
    }
}
