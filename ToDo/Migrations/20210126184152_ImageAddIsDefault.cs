using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDo.Migrations
{
    public partial class ImageAddIsDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Thumbnails");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Images",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Images");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Thumbnails",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
