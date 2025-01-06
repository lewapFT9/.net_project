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
		public int UserAccountId { get; set; }

        [Required(ErrorMessage = "Field is required.")]
		public bool IsConfirmed { get; set; } = false;

        [ForeignKey(nameof(ScreeningId))]
		public Screening Screening { get; set; }

        [ForeignKey(nameof(UserAccountId))]
		public UserAccount Account { get; set; }

    }
}
