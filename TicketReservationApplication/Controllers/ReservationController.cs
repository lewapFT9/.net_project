using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketReservationApplication.Entities;

namespace TicketReservationApplication.Controllers
{
    public class ReservationController : Controller
    {
        private readonly AppDbContext _context;

        public ReservationController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Reservate(int id)
        {
            var screening = await _context.Screenings.FirstOrDefaultAsync(s => s.Id == id);
            Console.WriteLine(id);
            if (screening == null)
            {
                return NotFound();
            }
            Reservation reservation = new Reservation();
            reservation.ScreeningId = screening.Id;
            Console.WriteLine(reservation.ScreeningId);

            return View(reservation);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Reservate(Reservation reservation) 
        {
            UserAccount userAccount = null;
            Reservation reservationToSave = new Reservation();
            userAccount = _context.UserAccounts.Where(ua => ua.Email == HttpContext.User.Identity.Name).FirstOrDefault();
            reservationToSave.UserAccountId = userAccount.Id;
            reservationToSave.ScreeningId= reservation.ScreeningId;
            Seat reservationSeat = new Seat();
            reservationSeat.ScreeningId = reservation.ScreeningId;
            reservationSeat.UserAccountId = userAccount.Id;
            Console.WriteLine(reservation.ScreeningId + " post");
            try
            {
                _context.Seats.Add(reservationSeat);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Seat SeatId = new Seat();
            SeatId = _context.Seats.Where(s=> s.ScreeningId == reservationSeat.ScreeningId  && s.UserAccountId == userAccount.Id 
            && s.IsConfirmed ==false).FirstOrDefault();

            if (SeatId == null || SeatId.Id == 0)
            {
                return BadRequest($"Seat could not be created.screening.Id: {reservation.ScreeningId}");
            }

            reservationToSave.SeatId = SeatId.Id;
            try
            {
                _context.Reservations.Add(reservationToSave);
                _context.SaveChanges();
                reservationSeat.IsConfirmed = true;
                _context.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = "Reservation has been made succesfully.";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View(reservation);
            }

            return View(reservation);
        }

    }
}
