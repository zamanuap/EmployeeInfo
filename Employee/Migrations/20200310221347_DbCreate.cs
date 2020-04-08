using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Employee.Migrations
{
    public partial class DbCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProvincePocos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvincePocos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePocos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: false),
                    Telephone = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Salary = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePocos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeePocos_ProvincePocos_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "ProvincePocos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePocos_ProvinceId",
                table: "EmployeePocos",
                column: "ProvinceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeePocos");

            migrationBuilder.DropTable(
                name: "ProvincePocos");
        }
    }
}
