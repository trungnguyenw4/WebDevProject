using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevProject.Migrations
{
    /// <inheritdoc />
    public partial class BackToLocal3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_C_C_CustomerId1",
                table: "C");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthInformation_C_CustomerId",
                table: "HealthInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceClaim_InsurancePolics_InsurancePolicyId",
                table: "InsuranceClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_InsurancePolics_C_CustomerId",
                table: "InsurancePolics");

            migrationBuilder.DropForeignKey(
                name: "FK_OccupationInformation_C_CustomerId",
                table: "OccupationInformation");

            migrationBuilder.DropTable(
                name: "FinancialInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InsurancePolics",
                table: "InsurancePolics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_C",
                table: "C");

            migrationBuilder.RenameTable(
                name: "InsurancePolics",
                newName: "InsurancePolicies");

            migrationBuilder.RenameTable(
                name: "C",
                newName: "Customers");

            migrationBuilder.RenameIndex(
                name: "IX_InsurancePolics_CustomerId",
                table: "InsurancePolicies",
                newName: "IX_InsurancePolicies_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_C_CustomerId1",
                table: "Customers",
                newName: "IX_Customers_CustomerId1");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "InsuranceClaim",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InsurancePolicies",
                table: "InsurancePolicies",
                column: "InsurancePolicyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "CustomerId");

            migrationBuilder.CreateTable(
                name: "FinancialStatus",
                columns: table => new
                {
                    FinancialInformationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    AnnualIncome = table.Column<decimal>(type: "TEXT", nullable: false),
                    Debts = table.Column<decimal>(type: "TEXT", nullable: true),
                    FinancialDependents = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialStatus", x => x.FinancialInformationId);
                    table.ForeignKey(
                        name: "FK_FinancialStatus_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceClaim_CustomerId",
                table: "InsuranceClaim",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialStatus_CustomerId",
                table: "FinancialStatus",
                column: "CustomerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Customers_CustomerId1",
                table: "Customers",
                column: "CustomerId1",
                principalTable: "Customers",
                principalColumn: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthInformation_Customers_CustomerId",
                table: "HealthInformation",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceClaim_Customers_CustomerId",
                table: "InsuranceClaim",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceClaim_InsurancePolicies_InsurancePolicyId",
                table: "InsuranceClaim",
                column: "InsurancePolicyId",
                principalTable: "InsurancePolicies",
                principalColumn: "InsurancePolicyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InsurancePolicies_Customers_CustomerId",
                table: "InsurancePolicies",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OccupationInformation_Customers_CustomerId",
                table: "OccupationInformation",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Customers_CustomerId1",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_HealthInformation_Customers_CustomerId",
                table: "HealthInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceClaim_Customers_CustomerId",
                table: "InsuranceClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceClaim_InsurancePolicies_InsurancePolicyId",
                table: "InsuranceClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_InsurancePolicies_Customers_CustomerId",
                table: "InsurancePolicies");

            migrationBuilder.DropForeignKey(
                name: "FK_OccupationInformation_Customers_CustomerId",
                table: "OccupationInformation");

            migrationBuilder.DropTable(
                name: "FinancialStatus");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceClaim_CustomerId",
                table: "InsuranceClaim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InsurancePolicies",
                table: "InsurancePolicies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "InsuranceClaim");

            migrationBuilder.RenameTable(
                name: "InsurancePolicies",
                newName: "InsurancePolics");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "C");

            migrationBuilder.RenameIndex(
                name: "IX_InsurancePolicies_CustomerId",
                table: "InsurancePolics",
                newName: "IX_InsurancePolics_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_CustomerId1",
                table: "C",
                newName: "IX_C_CustomerId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InsurancePolics",
                table: "InsurancePolics",
                column: "InsurancePolicyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_C",
                table: "C",
                column: "CustomerId");

            migrationBuilder.CreateTable(
                name: "FinancialInformation",
                columns: table => new
                {
                    FinancialInformationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    AnnualIncome = table.Column<decimal>(type: "TEXT", nullable: false),
                    Debts = table.Column<decimal>(type: "TEXT", nullable: true),
                    FinancialDependents = table.Column<int>(type: "INTEGER", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_FinancialInformation_CustomerId",
                table: "FinancialInformation",
                column: "CustomerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_C_C_CustomerId1",
                table: "C",
                column: "CustomerId1",
                principalTable: "C",
                principalColumn: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthInformation_C_CustomerId",
                table: "HealthInformation",
                column: "CustomerId",
                principalTable: "C",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceClaim_InsurancePolics_InsurancePolicyId",
                table: "InsuranceClaim",
                column: "InsurancePolicyId",
                principalTable: "InsurancePolics",
                principalColumn: "InsurancePolicyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InsurancePolics_C_CustomerId",
                table: "InsurancePolics",
                column: "CustomerId",
                principalTable: "C",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OccupationInformation_C_CustomerId",
                table: "OccupationInformation",
                column: "CustomerId",
                principalTable: "C",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
