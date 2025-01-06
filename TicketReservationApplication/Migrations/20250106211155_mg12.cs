using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketReservationApplication.Migrations
{
    /// <inheritdoc />
    public partial class mg12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_UserAccounts_UserAccountId",
                table: "Seats");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_UserAccounts_UserAccountId",
                table: "Seats",
                column: "UserAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_UserAccounts_UserAccountId",
                table: "Seats");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_UserAccounts_UserAccountId",
                table: "Seats",
                column: "UserAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
