using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SWAG.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    Code = table.Column<int>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Message = table.Column<string>(maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: true),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Type = table.Column<byte>(nullable: false),
                    Serialized = table.Column<string>(nullable: false),
                    Result = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Operation",
                columns: new[] { "Id", "CreatedOn", "ModifiedOn", "Result", "Serialized", "Type" },
                values: new object[] { new Guid("68f4e9c2-3592-4086-a952-89b76f4b432e"), new DateTime(2018, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, 8.0, "1;7", (byte)1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Operation");
        }
    }
}
