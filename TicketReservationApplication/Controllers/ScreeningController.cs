using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketReservationApplication.Entities;
using TicketReservationApplication.Models;

namespace TicketReservationApplication.Controllers
{
    public class ScreeningController : Controller
    {
        private readonly AppDbContext _context;

        public ScreeningController(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "ADMIN")]
        public IActionResult CreateScreening()
        {
            var films = _context.Movies
                .Select(f => new { f.Id, f.Title })
                .ToList();

            var halls = _context.CinemaHalls
                .Select(h => new { h.Id, h.Name })
                .ToList();

            ViewBag.Films = new SelectList(films, "Id", "Title");
            ViewBag.Halls = new SelectList(halls, "Id", "Name");

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public IActionResult CreateScreening(CreateScreeningModel model)
        {
            if (ModelState.IsValid)
            {
               
                Movie movie = new Movie();
                movie = _context.Movies.Where(m => m.Id == model.FilmId)
                    .FirstOrDefault();
                bool isConflict = _context.Screenings.Any(s =>
                   s.CinemaHallId == model.CinemaHallId &&
                   s.ScreeningDate < model.ScreeningDate.AddMinutes(movie.Duration) &&
                   model.ScreeningDate < s.EndDate);

                if (isConflict)
                {
                    ViewBag.Message1 = "This hall is already occupied at the given time";
                    ViewBag.Films = new SelectList(_context.Movies, "Id", "Title");
                    ViewBag.Halls = new SelectList(_context.CinemaHalls, "Id", "Name");
                    return View(model);
                }
                var screening = new Screening
                {
                    MovieId = model.FilmId,
                    CinemaHallId = model.CinemaHallId,
                    ScreeningDate = model.ScreeningDate,
                    EndDate = model.ScreeningDate.AddMinutes(movie.Duration)
                };

                try
                {
                    _context.Screenings.Add(screening);
                    _context.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = "Screening has been added succesfully.";
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    return View(model);
                }

                return View();
            }

            ViewBag.Films = new SelectList(_context.Movies, "Id", "Title");
            ViewBag.Halls = new SelectList(_context.CinemaHalls, "Id", "Name");
            return View(model);
        }

        [Authorize]
        public IActionResult GetScreenings() 
        {
            return View(_context.Screenings.ToList());
        }
        
        [Authorize]
        public async Task<IActionResult> ViewScreeningDetails(int id)
        {
            var screening = await _context.Screenings.FirstOrDefaultAsync(m => m.Id == id);

            if (screening == null)
            {
                return NotFound();
            }

            return View(screening);
        }

    }
}
