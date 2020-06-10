using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDo.Migrations
{
    public partial class ThumbnailChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Thumbnails_BoardId",
                table: "Thumbnails");

            migrationBuilder.DropColumn(
                name: "BoardId",
                table: "Thumbnails");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Thumbnails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnails_UserId",
                table: "Thumbnails",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Thumbnails_Users_UserId",
                table: "Thumbnails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Thumbnails_Users_UserId",
                table: "Thumbnails");

            migrationBuilder.DropIndex(
                name: "IX_Thumbnails_UserId",
                table: "Thumbnails");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Thumbnails");

            migrationBuilder.AddColumn<int>(
                name: "BoardId",
                table: "Thumbnails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnails_BoardId",
                table: "Thumbnails",
                column: "BoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Thumbnails_Boards_BoardId",
                table: "Thumbnails",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}