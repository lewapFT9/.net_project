using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TicketReservationApplication.Entities
{
	public class CinemaHall
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Field is required.")]
		[StringLength(50)]
		public string Name { get; set; }

		[Required(ErrorMessage = "Field is required.")]
		public int NumberOfRows { get; set; }

		[Required(ErrorMessage = "Field is required.")]
		public int SeatsPerRow { get; set; }
	}
}
