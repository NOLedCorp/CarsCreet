using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsCrete.Data.Migrations
{
    public partial class Mark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Mark",
                table: "Cars",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Mark",
                table: "Cars",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
