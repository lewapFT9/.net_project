using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketReservationApplication.Migrations
{
    /// <inheritdoc />
    public partial class mg11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Seats",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "Seats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Seats_UserAccountId",
                table: "Seats",
                column: "UserAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Seats_UserAccounts_UserAccountId",
                table: "Seats",
                column: "UserAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seats_UserAccounts_UserAccountId",
                table: "Seats");

            migrationBuilder.DropIndex(
                name: "IX_Seats_UserAccountId",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "Seats");
        }
    }
}
