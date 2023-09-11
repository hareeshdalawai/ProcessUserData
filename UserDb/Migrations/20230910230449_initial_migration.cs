using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserDb.Migrations
{
    /// <inheritdoc />
    public partial class initial_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotificationFlag = table.Column<bool>(type: "bit", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedTime", "DataValue", "Email", "NotificationFlag", "RecordId", "UpdatedTime", "UserName" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 9, 11, 1, 4, 49, 140, DateTimeKind.Local).AddTicks(170), "Some value", "user1@abc.com", false, 1, new DateTime(2023, 9, 11, 1, 4, 49, 140, DateTimeKind.Local).AddTicks(216), "User 1" },
                    { 2, new DateTime(2023, 9, 11, 1, 4, 49, 140, DateTimeKind.Local).AddTicks(220), "Some value", "user2@abc.com", false, 2, new DateTime(2023, 9, 11, 1, 4, 49, 140, DateTimeKind.Local).AddTicks(222), "User 2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
