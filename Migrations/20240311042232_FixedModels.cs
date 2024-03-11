using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevProject.Migrations
{
    /// <inheritdoc />
    public partial class FixedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceClaim_C_CustomerId",
                table: "InsuranceClaim");

            migrationBuilder.DropTable(
                name: "FinancialStatus");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceClaim_CustomerId",
                table: "InsuranceClaim");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "InsuranceClaim");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialInformation");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "InsuranceClaim",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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
                        name: "FK_FinancialStatus_C_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "C",
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
                name: "FK_InsuranceClaim_C_CustomerId",
                table: "InsuranceClaim",
                column: "CustomerId",
                principalTable: "C",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
