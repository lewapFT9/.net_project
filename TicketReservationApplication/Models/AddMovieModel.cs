using System.ComponentModel.DataAnnotations;

namespace TicketReservationApplication.Models
{
	public class AddMovieModel
	{

		[Required(ErrorMessage = "Field is required.")]
		[StringLength(100)]
		public string Title { get; set; }

		[StringLength(50)]
		public string Genre { get; set; }

		[StringLength(100)]
		public string Director { get; set; }

		[Required(ErrorMessage = "Field is required.")]
		public int Duration { get; set; } 

		[StringLength(50)]
		public string Language { get; set; }

		public string Description { get; set; } 

		[StringLength(10)]
		[RegularExpression(@"^PG\d{2}$",
			ErrorMessage = "Please Enter Age Restriction.")]
		public string AgeRestriction { get; set; }
	}
}
