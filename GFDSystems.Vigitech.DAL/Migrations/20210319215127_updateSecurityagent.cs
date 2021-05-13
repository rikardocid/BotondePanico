using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GFDSystems.Vigitech.DAL.Migrations
{
    public partial class updateSecurityagent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateRegister",
                table: "SecurityAgent",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Corporation",
                table: "SecurityAgent",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Rank",
                table: "SecurityAgent",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Corporation",
                table: "SecurityAgent");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "SecurityAgent");

            migrationBuilder.AlterColumn<int>(
                name: "DateRegister",
                table: "SecurityAgent",
                type: "int",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
