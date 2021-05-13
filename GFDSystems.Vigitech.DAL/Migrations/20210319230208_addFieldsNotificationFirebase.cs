using Microsoft.EntityFrameworkCore.Migrations;

namespace GFDSystems.Vigitech.DAL.Migrations
{
    public partial class addFieldsNotificationFirebase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "NotificationBases",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "LevelPriority",
                table: "NotificationBases",
                maxLength: 24,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "NotificationBases",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "NotificationBases",
                maxLength: 40,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "NotificationBases");

            migrationBuilder.DropColumn(
                name: "LevelPriority",
                table: "NotificationBases");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "NotificationBases");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "NotificationBases");
        }
    }
}
