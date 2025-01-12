using System.ComponentModel.DataAnnotations;

namespace TicketReservationApplication.Models
{
    public class ScreeningDetailsModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public String MovieTitle { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public String CinemaHallName { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public DateTime ScreeningDate { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        public int MovieId { get; set; }

    }
}
