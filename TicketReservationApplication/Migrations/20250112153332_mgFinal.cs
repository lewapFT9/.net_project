using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketReservationApplication.Migrations
{
    /// <inheritdoc />
    public partial class mgFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAnalysisDone",
                table: "Screenings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAnalysisDone",
                table: "Screenings");
        }
    }
}
