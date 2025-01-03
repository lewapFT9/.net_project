using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TicketReservationApplication.Entities;
using TicketReservationApplication.Models;

namespace TicketReservationApplication.Controllers
{
    public class AccountController : Controller
    {

        private readonly AppDbContext _context;

        public AccountController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

     

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
			
            model.Role = "USER";
		
			if (ModelState.IsValid)
            {
                UserAccount userAccount = new UserAccount();
                userAccount.Email = model.Email;
                userAccount.Surname = model.Surname;
                userAccount.Name = model.Name;
                userAccount.Role = "USER";
                userAccount.Password = model.HashPassword(model.Password);


                try
                {
                    _context.UserAccounts.Add(userAccount);
                    _context.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = $"{userAccount.Email} has been registered succesfully. Please log in.";
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Email is already used!");
                    return View(model);
                }

                return View();
            }
            else
            {
                return View(model);
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid) 
            {
                var userAccount = _context.UserAccounts.
                    Where(x => x.Email == model.Email && x.Password == model.HashPassword(model.Password)).FirstOrDefault();
                if (userAccount != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userAccount.Email),
                        new Claim("Name", userAccount.Name),
                        new Claim(ClaimTypes.Role, userAccount.Role),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("UserPage");
                }
                else
                {
                    ModelState.AddModelError("", "Wrong data");
                }
            }
            return View(model);
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [Authorize(Roles = "USER,ADMIN")]
        public IActionResult UserPage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }
    }
}
