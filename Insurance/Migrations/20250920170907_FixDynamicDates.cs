using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixDynamicDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 9, 20, 17, 9, 5, 625, DateTimeKind.Utc).AddTicks(6161) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 9, 20, 17, 9, 5, 625, DateTimeKind.Utc).AddTicks(8636) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 9, 20, 17, 9, 5, 625, DateTimeKind.Utc).AddTicks(8644) });

            migrationBuilder.UpdateData(
                table: "Policies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "EndDate", "LastModifiedDate", "StartDate" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 20, 17, 9, 5, 626, DateTimeKind.Utc).AddTicks(2045), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Policies",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "EndDate", "LastModifiedDate", "StartDate" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2044, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 20, 17, 9, 5, 627, DateTimeKind.Utc).AddTicks(2983), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Policies",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "EndDate", "LastModifiedDate", "StartDate" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 20, 17, 9, 5, 627, DateTimeKind.Utc).AddTicks(3029), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2025, 9, 20, 17, 5, 31, 603, DateTimeKind.Utc).AddTicks(939), new DateTime(2025, 9, 20, 17, 5, 31, 602, DateTimeKind.Utc).AddTicks(7901) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2025, 9, 20, 17, 5, 31, 603, DateTimeKind.Utc).AddTicks(1905), new DateTime(2025, 9, 20, 17, 5, 31, 603, DateTimeKind.Utc).AddTicks(1893) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2025, 9, 20, 17, 5, 31, 603, DateTimeKind.Utc).AddTicks(1913), new DateTime(2025, 9, 20, 17, 5, 31, 603, DateTimeKind.Utc).AddTicks(1907) });

            migrationBuilder.UpdateData(
                table: "Policies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "EndDate", "LastModifiedDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 20, 17, 5, 31, 603, DateTimeKind.Utc).AddTicks(9782), new DateTime(2026, 9, 20, 17, 5, 31, 603, DateTimeKind.Utc).AddTicks(7985), new DateTime(2025, 9, 20, 17, 5, 31, 603, DateTimeKind.Utc).AddTicks(5857), new DateTime(2025, 9, 20, 17, 5, 31, 603, DateTimeKind.Utc).AddTicks(7668) });

            migrationBuilder.UpdateData(
                table: "Policies",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "EndDate", "LastModifiedDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 20, 17, 5, 31, 604, DateTimeKind.Utc).AddTicks(90), new DateTime(2045, 9, 20, 17, 5, 31, 604, DateTimeKind.Utc).AddTicks(80), new DateTime(2025, 9, 20, 17, 5, 31, 604, DateTimeKind.Utc).AddTicks(74), new DateTime(2025, 9, 20, 17, 5, 31, 604, DateTimeKind.Utc).AddTicks(80) });

            migrationBuilder.UpdateData(
                table: "Policies",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "EndDate", "LastModifiedDate", "StartDate" },
                values: new object[] { new DateTime(2025, 9, 20, 17, 5, 31, 604, DateTimeKind.Utc).AddTicks(99), new DateTime(2026, 9, 20, 17, 5, 31, 604, DateTimeKind.Utc).AddTicks(96), new DateTime(2025, 9, 20, 17, 5, 31, 604, DateTimeKind.Utc).AddTicks(92), new DateTime(2025, 9, 20, 17, 5, 31, 604, DateTimeKind.Utc).AddTicks(95) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 9, 20, 17, 5, 31, 602, DateTimeKind.Utc).AddTicks(825));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 9, 20, 17, 5, 31, 602, DateTimeKind.Utc).AddTicks(1179));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 9, 20, 17, 5, 31, 602, DateTimeKind.Utc).AddTicks(1184));
        }
    }
}
