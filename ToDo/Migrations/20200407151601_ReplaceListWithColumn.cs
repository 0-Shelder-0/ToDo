using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDo.Migrations
{
    public partial class ReplaceListWithColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_Boards_ListId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_ListId",
                table: "Records");

            migrationBuilder.AddColumn<int>(
                name: "ColumnId",
                table: "Records",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Records_ColumnId",
                table: "Records",
                column: "ColumnId");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Lists_ColumnId",
                table: "Records",
                column: "ColumnId",
                principalTable: "Lists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_Lists_ColumnId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_ColumnId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "ColumnId",
                table: "Records");

            migrationBuilder.CreateIndex(
                name: "IX_Records_ListId",
                table: "Records",
                column: "ListId");

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
