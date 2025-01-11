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

		[Authorize]
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
            var referer = Request.Headers["Referer"].ToString();
            ViewBag.Referer = referer;

            return View(movie);
        }

		[Authorize(Roles = "ADMIN")]
		public IActionResult EditMovie(int id)
		{
			var movie = _context.Movies.FirstOrDefault(m => m.Id == id);
			if (movie == null)
			{
				return NotFound();
			}
			return View(movie);
		}
		[HttpPost]
		[Authorize(Roles = "ADMIN")]
		public IActionResult EditMovie(int id, Movie UpdatedMovie)
		{
			var movie = _context.Movies.FirstOrDefault(m => m.Id == id);
			if (movie == null)
			{
				return NotFound();
			}
			movie.Title = UpdatedMovie.Title;
			movie.Description = UpdatedMovie.Description;
			movie.Director = UpdatedMovie.Director;
			movie.AgeRestriction = UpdatedMovie.AgeRestriction;
			movie.Language = UpdatedMovie.Language;
			movie.Duration = UpdatedMovie.Duration;
			movie.Genre = UpdatedMovie.Genre;
			try
			{
				_context.SaveChanges();
				ModelState.Clear();
				ViewBag.Message = $"{movie.Title} has been edited succesfully.";
				return View(UpdatedMovie);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			return View(movie);
		}

    }

}
