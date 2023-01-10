using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PschoolAPIback.Migrations
{
    /// <inheritdoc />
    public partial class InsertedRoles2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a0e9ae5-4f2f-4498-b255-af3cc9403779");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be9a4cdb-b18c-458b-8188-bb80bb3038b2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "421aaae6-d410-47a0-aea0-7b9cf8e0b3bc", "8e6c0535-08b5-4c37-95a6-0fe858f7fa60", "admin", "ADMIN" },
                    { "8df9126d-41fd-46f2-ac10-008dbebd9b1d", "52268178-5bc8-45e0-9e03-a6e128a89fc4", "user", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "421aaae6-d410-47a0-aea0-7b9cf8e0b3bc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8df9126d-41fd-46f2-ac10-008dbebd9b1d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8a0e9ae5-4f2f-4498-b255-af3cc9403779", "b4c74a0d-e09c-4948-bacc-7fce684fedde", "Visitor", "VISITOR" },
                    { "be9a4cdb-b18c-458b-8188-bb80bb3038b2", "05167506-ed7a-46c2-b1ba-72d979a4fe77", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
