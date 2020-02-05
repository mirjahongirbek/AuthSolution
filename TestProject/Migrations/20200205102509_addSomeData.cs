using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestProject.Migrations
{
    public partial class addSomeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.AddColumn<string>(
                name: "changes",
                table: "Roles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "table_status",
                table: "Roles",
                nullable: false,
                defaultValue: 0);*/

            migrationBuilder.CreateTable(
                name: "DeleteData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Data = table.Column<string>(nullable: true),
                    TableName = table.Column<string>(nullable: true),
                    SchemeName = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeleteData", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeleteData");

            migrationBuilder.DropColumn(
                name: "changes",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "table_status",
                table: "Roles");
        }
    }
}
