using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsCrete.Data.Migrations
{
    public partial class Sales2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Sales_SaleId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_SaleId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "SaleId",
                table: "Books",
                newName: "SalesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SalesId",
                table: "Books",
                newName: "SaleId");

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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
