using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Employee.Migrations
{
    public partial class AddingTimeStampColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "TimeStamp",
                table: "EmployeePocos",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "EmployeePocos");
        }
    }
}
