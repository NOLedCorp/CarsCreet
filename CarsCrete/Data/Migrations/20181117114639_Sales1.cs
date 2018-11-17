using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsCrete.Data.Migrations
{
    public partial class Sales1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SaleId",
                table: "Books",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<double>(
                name: "Sum",
                table: "Books",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_SaleId",
                table: "Books",
                column: "SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Sales_SaleId",
                table: "Books",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Sales_SaleId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_SaleId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "SaleId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Sum",
                table: "Books");
        }
    }
}
