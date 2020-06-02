using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDo.Migrations
{
    public partial class AddBackgroundNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BackgroundNumber",
                table: "Boards",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundNumber",
                table: "Boards");
        }
    }
}
