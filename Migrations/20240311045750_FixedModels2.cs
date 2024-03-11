using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDevProject.Migrations
{
    /// <inheritdoc />
    public partial class FixedModels2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_C_C_CustomerId1",
                table: "C");

            migrationBuilder.DropIndex(
                name: "IX_C_CustomerId1",
                table: "C");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "C");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId1",
                table: "C",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_C_CustomerId1",
                table: "C",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_C_C_CustomerId1",
                table: "C",
                column: "CustomerId1",
                principalTable: "C",
                principalColumn: "CustomerId");
        }
    }
}
