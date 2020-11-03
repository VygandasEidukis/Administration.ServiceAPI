using Microsoft.EntityFrameworkCore.Migrations;

namespace EPS.Administration.DAL.Migrations
{
    public partial class DeviceAddedbaseIdrevisioning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BaseId",
                table: "Statuses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BaseId",
                table: "Devices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BaseId",
                table: "DeviceModels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BaseId",
                table: "DeviceEvents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BaseId",
                table: "Classifications",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseId",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "BaseId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "BaseId",
                table: "DeviceModels");

            migrationBuilder.DropColumn(
                name: "BaseId",
                table: "DeviceEvents");

            migrationBuilder.DropColumn(
                name: "BaseId",
                table: "Classifications");
        }
    }
}
