using Microsoft.EntityFrameworkCore.Migrations;

namespace EPS.Administration.DAL.Migrations
{
    public partial class DeviceAddedDeviceLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "DeviceEvents",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeviceLocations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseId = table.Column<int>(nullable: false),
                    Revision = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Details = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceLocations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceEvents_LocationId",
                table: "DeviceEvents",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceEvents_DeviceLocations_LocationId",
                table: "DeviceEvents",
                column: "LocationId",
                principalTable: "DeviceLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceEvents_DeviceLocations_LocationId",
                table: "DeviceEvents");

            migrationBuilder.DropTable(
                name: "DeviceLocations");

            migrationBuilder.DropIndex(
                name: "IX_DeviceEvents_LocationId",
                table: "DeviceEvents");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "DeviceEvents");
        }
    }
}
