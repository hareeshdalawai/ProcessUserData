using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserDb.Migrations
{
    /// <inheritdoc />
    public partial class create_sp_dbo_get_users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var procedure = @"CREATE PROCEDURE [dbo].[GetUsers]
                        (
                            @LastExecutionTime DATETIME
                        )
                        AS
                        BEGIN
    
                            SELECT 
		                        RecordId,
		                        UserId,
		                        UserName,
		                        Email,
                                DataValue,
                                NotificationFlag,
                                CreatedTime,
                                UpdatedTime
	                        FROM [dbo].[Users]
	                        WHERE CreatedTime >= @LastExecutionTime OR UpdatedTime >= @LastExecutionTime
                        END";

            migrationBuilder.Sql(procedure);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2023, 9, 11, 1, 4, 49, 140, DateTimeKind.Local).AddTicks(170), new DateTime(2023, 9, 11, 1, 4, 49, 140, DateTimeKind.Local).AddTicks(216) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "CreatedTime", "UpdatedTime" },
                values: new object[] { new DateTime(2023, 9, 11, 1, 4, 49, 140, DateTimeKind.Local).AddTicks(220), new DateTime(2023, 9, 11, 1, 4, 49, 140, DateTimeKind.Local).AddTicks(222) });
        }
    }
}
