using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedagentidtousers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_AgentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AgentId",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_AgentId",
                table: "Users",
                column: "AgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_AgentId",
                table: "Users",
                column: "AgentId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
