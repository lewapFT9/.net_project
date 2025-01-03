namespace TicketReservationApplication.Models
{
    public class CreateScreeningModel
    {
        public int FilmId { get; set; }
        public int CinemaHallId { get; set; }
        public DateTime ScreeningDate { get; set; }
    }
}
