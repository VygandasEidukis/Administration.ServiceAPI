using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EPS.Administration.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Revision = table.Column<int>(nullable: false),
                    BaseId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classifications", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "DeviceModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Revision = table.Column<int>(nullable: false),
                    BaseId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Revision = table.Column<int>(nullable: false),
                    BaseId = table.Column<int>(nullable: false),
                    SerialNumber = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    ModelId = table.Column<int>(nullable: false),
                    AcquisitionDate = table.Column<DateTime>(nullable: false),
                    InvoiceNumber = table.Column<string>(nullable: true),
                    ClassificationId = table.Column<int>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    OwnedById = table.Column<int>(nullable: false),
                    InitialLocationId = table.Column<int>(nullable: false),
                    SfDate = table.Column<DateTime>(nullable: false),
                    SfNumber = table.Column<string>(nullable: true),
                    AdditionalNotes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Revision = table.Column<int>(nullable: false),
                    BaseId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceEvents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Revision = table.Column<int>(nullable: false),
                    BaseId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: true),
                    DeviceDataId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceEvents_Devices_DeviceDataId",
                        column: x => x.DeviceDataId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceEvents_DeviceDataId",
                table: "DeviceEvents",
                column: "DeviceDataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Classifications");

            migrationBuilder.DropTable(
                name: "DeviceEvents");

            migrationBuilder.DropTable(
                name: "DeviceLocations");

            migrationBuilder.DropTable(
                name: "DeviceModels");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
