using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsCrete.Data.Migrations
{
    public partial class Mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_FeedBack_FeedBackId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "Lang",
                table: "Users",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "FeedBackId",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Books",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_FeedBack_FeedBackId",
                table: "Comments",
                column: "FeedBackId",
                principalTable: "FeedBack",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_FeedBack_FeedBackId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Lang",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Books");

            migrationBuilder.AlterColumn<long>(
                name: "FeedBackId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "ReportId",
                table: "Comments",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_FeedBack_FeedBackId",
                table: "Comments",
                column: "FeedBackId",
                principalTable: "FeedBack",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
