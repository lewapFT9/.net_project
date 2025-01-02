using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketReservationApplication.Entities
{
	public class Seat
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Field is required.")]
		public int ScreeningId { get; set; }

		[Required(ErrorMessage = "Field is required.")]
		public int RowNumber { get; set; }

		[Required(ErrorMessage = "Field is required.")]
		public int SeatNumber { get; set; }

		[Required(ErrorMessage = "Field is required.")]
		public bool IsOccupied { get; set; } = false; // Default value as in SQL

		// Navigation property
		[ForeignKey(nameof(ScreeningId))]
		public Screening Screening { get; set; }
	}
}
