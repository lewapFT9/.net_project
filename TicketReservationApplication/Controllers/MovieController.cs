using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketReservationApplication.Entities;
using TicketReservationApplication.Models;

namespace TicketReservationApplication.Controllers
{
	public class MovieController : Controller
	{
		private readonly AppDbContext _context;

		public MovieController(AppDbContext appDbContext)
		{
			_context = appDbContext;
		}

		public IActionResult Index()
		{
			return View();
		}

        [Authorize(Roles = "ADMIN")]
        public IActionResult AddMovie()
		{
			return View();
		}

		[HttpPost]
		public IActionResult AddMovie(AddMovieModel model) 
		{
			if (ModelState.IsValid)
			{
				Movie movie = new Movie();
				movie.Title = model.Title;
				movie.Director = model.Director;
				movie.Duration = model.Duration;
				movie.Genre = model.Genre;
				movie.Language = model.Language;
				movie.AgeRestriction = model.AgeRestriction;
				movie.Description = model.Description;

				try
				{
					_context.Movies.Add(movie);
					_context.SaveChanges();


					ModelState.Clear();
					ViewBag.Message = $"{movie.Title} has been added succesfully.";
				}
				catch (Exception ex)
				{
                    Console.WriteLine("Error: "+ex.Message);
					return View(model);
				}
				return View();
			}
			return View(model);
		}

		//[Authorize(Roles = "ADMIN, USER")]
		public IActionResult ViewMovies() 
		{ 
			return View(_context.Movies.ToList());
		}


		public async Task<IActionResult> ViewMovieDetails(int id)
		{
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound(); 
            }

            return View(movie);
        }
    }

}
