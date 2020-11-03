using Microsoft.EntityFrameworkCore.Migrations;

namespace EPS.Administration.DAL.Migrations
{
    public partial class DeviceStructureUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceEvents_Devices_DeviceDataSerialNumber",
                table: "DeviceEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Devices",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_DeviceEvents_DeviceDataSerialNumber",
                table: "DeviceEvents");

            migrationBuilder.DropColumn(
                name: "DeviceDataSerialNumber",
                table: "DeviceEvents");

            migrationBuilder.AddColumn<int>(
                name: "Revision",
                table: "Statuses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Devices",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Devices",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Revision",
                table: "Devices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Revision",
                table: "DeviceModels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeviceDataId",
                table: "DeviceEvents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Revision",
                table: "DeviceEvents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Revision",
                table: "Classifications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Devices",
                table: "Devices",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceEvents_DeviceDataId",
                table: "DeviceEvents",
                column: "DeviceDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceEvents_Devices_DeviceDataId",
                table: "DeviceEvents",
                column: "DeviceDataId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceEvents_Devices_DeviceDataId",
                table: "DeviceEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Devices",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_DeviceEvents_DeviceDataId",
                table: "DeviceEvents");

            migrationBuilder.DropColumn(
                name: "Revision",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Revision",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Revision",
                table: "DeviceModels");

            migrationBuilder.DropColumn(
                name: "DeviceDataId",
                table: "DeviceEvents");

            migrationBuilder.DropColumn(
                name: "Revision",
                table: "DeviceEvents");

            migrationBuilder.DropColumn(
                name: "Revision",
                table: "Classifications");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Devices",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeviceDataSerialNumber",
                table: "DeviceEvents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Devices",
                table: "Devices",
                column: "SerialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceEvents_DeviceDataSerialNumber",
                table: "DeviceEvents",
                column: "DeviceDataSerialNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceEvents_Devices_DeviceDataSerialNumber",
                table: "DeviceEvents",
                column: "DeviceDataSerialNumber",
                principalTable: "Devices",
                principalColumn: "SerialNumber",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
