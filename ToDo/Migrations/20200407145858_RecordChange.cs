using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDo.Migrations
{
    public partial class RecordChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_Boards_ListId",
                table: "Records");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Lists_ListId",
                table: "Records",
                column: "ListId",
                principalTable: "Lists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_Lists_ListId",
                table: "Records");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Boards_ListId",
                table: "Records",
                column: "ListId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
