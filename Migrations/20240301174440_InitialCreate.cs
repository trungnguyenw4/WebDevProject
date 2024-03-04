using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "C",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Gender = table.Column<string>(type: "TEXT", nullable: true),
                    MaritalStatus = table.Column<string>(type: "TEXT", nullable: false),
                    ContactNumber = table.Column<string>(type: "TEXT", nullable: false),
                    EmailAddress = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerId1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_C", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_C_C_CustomerId1",
                        column: x => x.CustomerId1,
                        principalTable: "C",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "FinancialInformation",
                columns: table => new
                {
                    FinancialInformationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    AnnualIncome = table.Column<decimal>(type: "TEXT", nullable: false),
                    Debts = table.Column<decimal>(type: "TEXT", nullable: false),
                    FinancialDependents = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialInformation", x => x.FinancialInformationId);
                    table.ForeignKey(
                        name: "FK_FinancialInformation_C_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "C",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthInformation",
                columns: table => new
                {
                    HealthInformationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    MedicalHistory = table.Column<string>(type: "TEXT", nullable: false),
                    CurrentHealthStatus = table.Column<string>(type: "TEXT", nullable: false),
                    LifestyleHabits = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthInformation", x => x.HealthInformationId);
                    table.ForeignKey(
                        name: "FK_HealthInformation_C_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "C",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InsurancePolics",
                columns: table => new
                {
                    InsurancePolicyId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    CoverageAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PremiumAmount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurancePolics", x => x.InsurancePolicyId);
                    table.ForeignKey(
                        name: "FK_InsurancePolics_C_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "C",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OccupationInformation",
                columns: table => new
                {
                    OccupationInformationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Occupation = table.Column<string>(type: "TEXT", nullable: false),
                    EmployerName = table.Column<string>(type: "TEXT", nullable: false),
                    EmploymentStability = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OccupationInformation", x => x.OccupationInformationId);
                    table.ForeignKey(
                        name: "FK_OccupationInformation_C_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "C",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceClaim",
                columns: table => new
                {
                    InsuranceClaimId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InsurancePolicyId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClaimDetails = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ClaimAmount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceClaim", x => x.InsuranceClaimId);
                    table.ForeignKey(
                        name: "FK_InsuranceClaim_InsurancePolics_InsurancePolicyId",
                        column: x => x.InsurancePolicyId,
                        principalTable: "InsurancePolics",
                        principalColumn: "InsurancePolicyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_C_CustomerId1",
                table: "C",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialInformation_CustomerId",
                table: "FinancialInformation",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HealthInformation_CustomerId",
                table: "HealthInformation",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceClaim_InsurancePolicyId",
                table: "InsuranceClaim",
                column: "InsurancePolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolics_CustomerId",
                table: "InsurancePolics",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OccupationInformation_CustomerId",
                table: "OccupationInformation",
                column: "CustomerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialInformation");

            migrationBuilder.DropTable(
                name: "HealthInformation");

            migrationBuilder.DropTable(
                name: "InsuranceClaim");

            migrationBuilder.DropTable(
                name: "OccupationInformation");

            migrationBuilder.DropTable(
                name: "InsurancePolics");

            migrationBuilder.DropTable(
                name: "C");
        }
    }
}
