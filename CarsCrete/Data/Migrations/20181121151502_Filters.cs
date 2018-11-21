using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsCrete.Data.Migrations
{
    public partial class Filters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BodyType",
                table: "Cars",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FilterProp",
                table: "Cars",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BodyType",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "FilterProp",
                table: "Cars");
        }
    }
}
