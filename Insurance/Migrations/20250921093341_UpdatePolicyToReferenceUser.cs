using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InsuranceAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePolicyToReferenceUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Customers_CustomerId",
                table: "Policies");

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

            migrationBuilder.AlterColumn<int>(
                name: "AgentId",
                table: "Policies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId1",
                table: "Policies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Policies_CustomerId1",
                table: "Policies",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Customers_CustomerId1",
                table: "Policies",
                column: "CustomerId1",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Users_CustomerId",
                table: "Policies",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Customers_CustomerId1",
                table: "Policies");

            migrationBuilder.DropForeignKey(
                name: "FK_Policies_Users_CustomerId",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Policies_CustomerId1",
                table: "Policies");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "Policies");

            migrationBuilder.AlterColumn<int>(
                name: "AgentId",
                table: "Policies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Policies",
                columns: new[] { "Id", "AgentId", "CoverageAmount", "CreatedDate", "CustomerId", "Deductible", "EndDate", "LastModifiedDate", "Notes", "PolicyNumber", "PolicyType", "PremiumAmount", "StartDate", "Status" },
                values: new object[,]
                {
                    { 1, 2, 500000m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, 5000m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "POL-2024-000001", "Health", 25000m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active" },
                    { 2, 2, 2000000m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, 0m, new DateTime(2044, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "POL-2024-000002", "Life", 50000m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active" },
                    { 3, 2, 300000m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, 2000m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "POL-2024-000003", "Vehicle", 15000m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Policies_Customers_CustomerId",
                table: "Policies",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
