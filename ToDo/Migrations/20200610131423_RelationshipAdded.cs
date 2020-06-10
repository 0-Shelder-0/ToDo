using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDo.Migrations
{
    public partial class RelationshipAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Boards",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boards_ImageId",
                table: "Boards",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Images_ImageId",
                table: "Boards",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Images_ImageId",
                table: "Boards");

            migrationBuilder.DropIndex(
                name: "IX_Boards_ImageId",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Boards");
        }
    }
}
