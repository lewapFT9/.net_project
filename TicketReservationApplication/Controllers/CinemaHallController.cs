using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketReservationApplication.Entities;
using TicketReservationApplication.Models;

namespace TicketReservationApplication.Controllers
{
    public class CinemaHallController : Controller
    {
        private readonly AppDbContext _context;

        public CinemaHallController (AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "ADMIN")]
        public IActionResult AddCinemaHall()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCinemaHall(AddCinemaHallModel model)
        {
            if (ModelState.IsValid)
            {
                CinemaHall cinemaHall = new CinemaHall();
                cinemaHall.Name = model.Name;
                cinemaHall.NumberOfRows = model.NumberOfRows;
                cinemaHall.SeatsPerRow = model.SeatsPerRow;

                try
                {
                    _context.CinemaHalls.Add(cinemaHall);
                    _context.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = $"Hall {cinemaHall.Name} has been added succesfully.";

                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Name is already used!");
                    return View(model);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return View();
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult ViewCinemaHalls()
        {
            return View(_context.CinemaHalls.ToList());
        }


        [Authorize(Roles = "ADMIN")]
        public IActionResult EditCinemaHall(int id)
        {
            var cinemaHall = _context.CinemaHalls.FirstOrDefault(ch => ch.Id == id);

            if (cinemaHall == null)
            {
                Console.WriteLine("pierwszy if w get");
                return NotFound();
            }

            return View(cinemaHall);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public IActionResult EditCinemaHall(int id, CinemaHall updatedCinemaHall)
        {
            var cinemaHall = _context.CinemaHalls.FirstOrDefault(ch => ch.Id == id);
            if (cinemaHall == null)
            {
                return NotFound();
            }

            cinemaHall.Name = updatedCinemaHall.Name;
            cinemaHall.NumberOfRows = updatedCinemaHall.NumberOfRows;
            cinemaHall.SeatsPerRow = updatedCinemaHall.SeatsPerRow;

            try
            {
                _context.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = $"Hall {updatedCinemaHall.Name} has been edited succesfully.";
                return View(updatedCinemaHall);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(cinemaHall);
        }



    }
}
