using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsCrete.Data.Migrations
{
    public partial class CarDetails_Photos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilterProp",
                table: "Cars");

            migrationBuilder.AddColumn<bool>(
                name: "ABS",
                table: "Cars",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Airbags",
                table: "Cars",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Contain",
                table: "Cars",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Groupe",
                table: "Cars",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MinAge",
                table: "Cars",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Radio",
                table: "Cars",
                nullable: false,
                defaultValue: false);
            

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PhotoOwnerId = table.Column<long>(nullable: false),
                    Path = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                });
            migrationBuilder.CreateIndex(
                name: "IX_Photos_PhotoOwnerId",
                table: "Photos",
                column: "PhotoOwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropColumn(
                name: "ABS",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Airbags",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Contain",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Groupe",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "MinAge",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Radio",
                table: "Cars");
            migrationBuilder.DropIndex(
                name: "IX_Photos_PhotoOwnerId",
                table: "Photos");
            migrationBuilder.AddColumn<int>(
                name: "FilterProp",
                table: "Cars",
                nullable: false,
                defaultValue: 0);
        }
    }
}
