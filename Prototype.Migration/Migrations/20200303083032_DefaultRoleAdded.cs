using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prototype.DatabaseMigration.Migrations
{
    public partial class DefaultRoleAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("e95953b5-d4b5-4f02-8b92-27e9c5a3218d"), "f8691cfa-2dec-4d50-9f6a-3558c2b8d3b4", "admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e95953b5-d4b5-4f02-8b92-27e9c5a3218d"));
        }
    }
}
