using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InsuranceAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedvalues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "State", "Street", "ZipCode" },
                values: new object[,]
                {
                    { 1, "Mumbai", "India", "Maharashtra", "123 MG Road", "400001" },
                    { 2, "Bangalore", "India", "Karnataka", "456 Brigade Road", "560001" },
                    { 3, "Chennai", "India", "Tamil Nadu", "789 Anna Salai", "600001" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "FirstName", "IsActive", "LastLoginDate", "LastName", "PasswordHash", "PhoneNumber", "RoleId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 20, 17, 5, 31, 602, DateTimeKind.Utc).AddTicks(825), "admin@insurance.com", "Rajesh", true, null, "Kumar", "Admin123!", "9876543210", 1 },
                    { 2, new DateTime(2025, 9, 20, 17, 5, 31, 602, DateTimeKind.Utc).AddTicks(1179), "priya.agent@insurance.com", "Priya", true, null, "Sharma", "Agent123!", "9876543211", 2 },
                    { 3, new DateTime(2025, 9, 20, 17, 5, 31, 602, DateTimeKind.Utc).AddTicks(1184), "amit.manager@insurance.com", "Amit", true, null, "Patel", "Manager123!", "9876543212", 4 }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "AddressId", "CreatedDate", "DateOfBirth", "DriversLicenseNumber", "Email", "FirstName", "LastModifiedDate", "LastName", "PhoneNumber", "SocialSecurityNumber" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 9, 20, 17, 5, 31, 603, DateTimeKind.Utc).AddTicks(939), new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "arjun.singh@gmail.com", "Arjun", new DateTime(2025, 9, 20, 17, 5, 31, 602, DateTimeKind.Utc).AddTicks(7901), "Singh", "9876543213", null },
                    { 2, 2, new DateTime(2025, 9, 20, 17, 5, 31, 603, DateTimeKind.Utc).AddTicks(1905), new DateTime(1990, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "kavya.reddy@gmail.com", "Kavya", new DateTime(2025, 9, 20, 17, 5, 31, 603, DateTimeKind.Utc).AddTicks(1893), "Reddy", "9876543214", null },
                    { 3, 3, new DateTime(2025, 9, 20, 17, 5, 31, 603, DateTimeKind.Utc).AddTicks(1913), new DateTime(1988, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "vikram.gupta@gmail.com", "Vikram", new DateTime(2025, 9, 20, 17, 5, 31, 603, DateTimeKind.Utc).AddTicks(1907), "Gupta", "9876543215", null }
                });

            migrationBuilder.InsertData(
                table: "Policies",
                columns: new[] { "Id", "AgentId", "CoverageAmount", "CreatedDate", "CustomerId", "Deductible", "EndDate", "LastModifiedDate", "Notes", "PolicyNumber", "PolicyType", "PremiumAmount", "StartDate", "Status" },
                values: new object[,]
                {
                    { 1, 2, 500000m, new DateTime(2025, 9, 20, 17, 5, 31, 603, DateTimeKind.Utc).AddTicks(9782), 1, 5000m, new DateTime(2026, 9, 20, 17, 5, 31, 603, DateTimeKind.Utc).AddTicks(7985), new DateTime(2025, 9, 20, 17, 5, 31, 603, DateTimeKind.Utc).AddTicks(5857), null, "POL-2024-000001", "Health", 25000m, new DateTime(2025, 9, 20, 17, 5, 31, 603, DateTimeKind.Utc).AddTicks(7668), "Active" },
                    { 2, 2, 2000000m, new DateTime(2025, 9, 20, 17, 5, 31, 604, DateTimeKind.Utc).AddTicks(90), 2, 0m, new DateTime(2045, 9, 20, 17, 5, 31, 604, DateTimeKind.Utc).AddTicks(80), new DateTime(2025, 9, 20, 17, 5, 31, 604, DateTimeKind.Utc).AddTicks(74), null, "POL-2024-000002", "Life", 50000m, new DateTime(2025, 9, 20, 17, 5, 31, 604, DateTimeKind.Utc).AddTicks(80), "Active" },
                    { 3, 2, 300000m, new DateTime(2025, 9, 20, 17, 5, 31, 604, DateTimeKind.Utc).AddTicks(99), 3, 2000m, new DateTime(2026, 9, 20, 17, 5, 31, 604, DateTimeKind.Utc).AddTicks(96), new DateTime(2025, 9, 20, 17, 5, 31, 604, DateTimeKind.Utc).AddTicks(92), null, "POL-2024-000003", "Vehicle", 15000m, new DateTime(2025, 9, 20, 17, 5, 31, 604, DateTimeKind.Utc).AddTicks(95), "Active" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Policies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Policies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Policies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
