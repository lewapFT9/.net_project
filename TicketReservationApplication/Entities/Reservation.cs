using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketReservationApplication.Entities
{
	public class Reservation
	{
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

		[Required(ErrorMessage = "Field is required.")]
		public int SeatId { get; set; }

		[Required(ErrorMessage = "Field is required.")]
		public int UserAccountId { get; set; } 

		[Required(ErrorMessage = "Field is required.")]
		public int ScreeningId { get; set; }

		[ForeignKey(nameof(SeatId))]
		public Seat Seat { get; set; }

		[ForeignKey(nameof(UserAccountId))]
		public UserAccount UserAccount { get; set; }

		[ForeignKey(nameof(ScreeningId))]
		public Screening Screening { get; set; }
	}
}
