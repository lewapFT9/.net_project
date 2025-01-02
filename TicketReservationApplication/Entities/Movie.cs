using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TicketReservationApplication.Entities
{
	public class Movie
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Field is required.")]
		[StringLength(100)]
		public string Title { get; set; }

		[StringLength(50)]
		public string Genre { get; set; }

		[StringLength(100)]
		public string Director { get; set; }

		[Required(ErrorMessage = "Field is required.")]
		public int Duration { get; set; } // Representing "CzasTrwania"

		[StringLength(50)]
		public string Language { get; set; }

		public string Description { get; set; } // Representing "Opis"

		[StringLength(10)]
		public string AgeRestriction { get; set; } // Representing "OgraniczeniaWiekowe"
	}
}
