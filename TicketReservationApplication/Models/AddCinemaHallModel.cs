using System.ComponentModel.DataAnnotations;

namespace TicketReservationApplication.Models
{
    public class AddCinemaHallModel
    {
        [Required(ErrorMessage = "Field is required.")]
        [StringLength(50)]
        [RegularExpression(@"^[A-Z]\d{1,2}$", ErrorMessage = "Name must start with one uppercase letter followed by 1 or 2 digits.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public int NumberOfRows { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public int SeatsPerRow { get; set; }
    }
}
