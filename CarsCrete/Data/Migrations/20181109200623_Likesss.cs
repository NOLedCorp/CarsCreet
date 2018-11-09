using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsCrete.Data.Migrations
{
    public partial class Likesss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Comments_ReportCommentId",
                table: "Likes");

            migrationBuilder.AlterColumn<long>(
                name: "ReportCommentId",
                table: "Likes",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "CommentId",
                table: "Likes",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Comments_ReportCommentId",
                table: "Likes",
                column: "ReportCommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Comments_ReportCommentId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "Likes");

            migrationBuilder.AlterColumn<long>(
                name: "ReportCommentId",
                table: "Likes",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Comments_ReportCommentId",
                table: "Likes",
                column: "ReportCommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
