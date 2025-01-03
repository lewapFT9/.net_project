using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketReservationApplication.Entities
{
	public class Screening
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Field is required.")]
		public int MovieId { get; set; }

		[Required(ErrorMessage = "Field is required.")]
		public int CinemaHallId { get; set; }

		[Required(ErrorMessage = "Field is required.")]
		public DateTime ScreeningDate { get; set; }

		[Required(ErrorMessage = "Field is required.")]
		public DateTime EndDate { get; set; }

		[ForeignKey(nameof(MovieId))]
		public Movie Movie { get; set; }

		[ForeignKey(nameof(CinemaHallId))]
		public CinemaHall CinemaHall { get; set; }
	}
}
