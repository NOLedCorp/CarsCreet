using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CarsCrete.Data.Migrations
{
    public partial class Init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Books",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
            migrationBuilder.AddColumn<string>(
                name: "Lang",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "CreateDate",
               table: "Books");
            migrationBuilder.DropColumn(
               name: "Lang",
               table: "Users");

        }
    }
}
