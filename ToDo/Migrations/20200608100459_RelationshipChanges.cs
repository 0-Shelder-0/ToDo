using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDo.Migrations
{
    public partial class RelationshipChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Boards_BoardId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Thumbnails_Images_ImageId",
                table: "Thumbnails");

            migrationBuilder.DropIndex(
                name: "IX_Thumbnails_ImageId",
                table: "Thumbnails");

            migrationBuilder.DropIndex(
                name: "IX_Images_BoardId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Thumbnails");

            migrationBuilder.DropColumn(
                name: "BoardId",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "ThumbnailId",
                table: "Images",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Boards",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ThumbnailId",
                table: "Images",
                column: "ThumbnailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boards_ImageId",
                table: "Boards",
                column: "ImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Images_ImageId",
                table: "Boards",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Thumbnails_ThumbnailId",
                table: "Images",
                column: "ThumbnailId",
                principalTable: "Thumbnails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Images_ImageId",
                table: "Boards");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Thumbnails_ThumbnailId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ThumbnailId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Boards_ImageId",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "ThumbnailId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Boards");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Thumbnails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BoardId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Thumbnails_ImageId",
                table: "Thumbnails",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_BoardId",
                table: "Images",
                column: "BoardId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Boards_BoardId",
                table: "Images",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Thumbnails_Images_ImageId",
                table: "Thumbnails",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
