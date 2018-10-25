using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsCrete.Data.Migrations
{
    public partial class Initial5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Mark",
                table: "FeedBack",
                nullable: false,
                oldClrType: typeof(short));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Cars_CarId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_CarId",
                table: "Books");

            migrationBuilder.AlterColumn<short>(
                name: "Mark",
                table: "FeedBack",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
