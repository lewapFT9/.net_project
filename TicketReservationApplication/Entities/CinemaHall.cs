using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;

namespace TicketReservationApplication.Entities
{
    [Index(nameof(Name), IsUnique = true)]
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
