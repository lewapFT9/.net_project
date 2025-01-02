using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketReservationApplication.Entities
{
	public class Reservation
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Field is required.")]
		public int SeatId { get; set; }

		[Required(ErrorMessage = "Field is required.")]
		public int UserAccountId { get; set; } // Zastąpione "UzytkownikId"

		[Required(ErrorMessage = "Field is required.")]
		public DateTime ReservationDate { get; set; } = DateTime.Now;

		// Navigation properties
		[ForeignKey(nameof(SeatId))]
		public Seat Seat { get; set; }

		[ForeignKey(nameof(UserAccountId))]
		public UserAccount UserAccount { get; set; }
	}
}
