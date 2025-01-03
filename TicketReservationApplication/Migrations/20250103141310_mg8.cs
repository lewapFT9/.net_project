using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketReservationApplication.Migrations
{
    /// <inheritdoc />
    public partial class mg8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CinemaHalls_Name",
                table: "CinemaHalls",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CinemaHalls_Name",
                table: "CinemaHalls");
        }
    }
}
