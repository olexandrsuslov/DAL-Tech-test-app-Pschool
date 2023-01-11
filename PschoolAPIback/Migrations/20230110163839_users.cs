using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PschoolAPIback.Migrations
{
    /// <inheritdoc />
    public partial class users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "421aaae6-d410-47a0-aea0-7b9cf8e0b3bc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8df9126d-41fd-46f2-ac10-008dbebd9b1d");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0d6e6360-12e3-4ce4-a6cc-10d6ab25d589", "c6f7f639-7a2a-419f-8b69-177b24776ea0", "user", "USER" },
                    { "95c48514-8960-45f5-8e5f-cfbed99e0bf2", "26785073-3192-4e73-a1cb-98774c15f3ae", "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d6e6360-12e3-4ce4-a6cc-10d6ab25d589");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95c48514-8960-45f5-8e5f-cfbed99e0bf2");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "421aaae6-d410-47a0-aea0-7b9cf8e0b3bc", "8e6c0535-08b5-4c37-95a6-0fe858f7fa60", "admin", "ADMIN" },
                    { "8df9126d-41fd-46f2-ac10-008dbebd9b1d", "52268178-5bc8-45e0-9e03-a6e128a89fc4", "user", "USER" }
                });
        }
    }
}
