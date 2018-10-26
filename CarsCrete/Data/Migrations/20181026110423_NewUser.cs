using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsCrete.Data.Migrations
{
    public partial class NewUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Comfort",
                table: "FeedBack",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "FeedBack",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Drive",
                table: "FeedBack",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "FeedBack",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Look",
                table: "FeedBack",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(nullable: false),
                    ReportId = table.Column<long>(nullable: false),
                    Likes = table.Column<int>(nullable: false),
                    Dislikes = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    FeedBackId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_FeedBack_FeedBackId",
                        column: x => x.FeedBackId,
                        principalTable: "FeedBack",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_FeedBackId",
                table: "Comments",
                column: "FeedBackId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Comfort",
                table: "FeedBack");

            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "FeedBack");

            migrationBuilder.DropColumn(
                name: "Drive",
                table: "FeedBack");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "FeedBack");

            migrationBuilder.DropColumn(
                name: "Look",
                table: "FeedBack");
        }
    }
}
