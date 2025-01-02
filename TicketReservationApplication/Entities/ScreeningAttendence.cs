using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketReservationApplication.Entities
{
	public class ScreeningAttendance
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Field is required.")]
		public int ScreeningId { get; set; }

		[Required(ErrorMessage = "Field is required.")]
		public int MovieId { get; set; }

		[Required(ErrorMessage = "Field is required.")]
		public int TotalSeats { get; set; }

		[Required(ErrorMessage = "Field is required.")]
		public int OccupiedSeats { get; set; }

		[Column(TypeName = "DECIMAL(5, 2)")]
		public decimal AttendancePercentage { get; set; } // Just a regular column in the database

		// Navigation properties
		[ForeignKey(nameof(ScreeningId))]
		public Screening Screening { get; set; }

		[ForeignKey(nameof(MovieId))]
		public Movie Movie { get; set; }
	}
}
