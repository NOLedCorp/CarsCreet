using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsCrete.Data.Migrations
{
    public partial class Initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Cars_CarId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_CarId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "Consumption",
                table: "Cars",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Description_ENG",
                table: "Cars",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description_ENG",
                table: "Cars");

            migrationBuilder.AlterColumn<string>(
                name: "Consumption",
                table: "Cars",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Books_CarId",
                table: "Books",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Cars_CarId",
                table: "Books",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
