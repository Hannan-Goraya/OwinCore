using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OwinCore.data;
using OwinCore.dto;
using OwinCore.Models;
using System.Diagnostics;
using System.Security.Claims;


namespace OwinCore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppAuthContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, AppAuthContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest req)
        {
            var user = new AppUsers { 
                Email = req.Email,
                Password = req.Password,
            };

            var users = _context.AppUsers.FirstOrDefault(x => x.Email == req.Email);


            var claim = new List<Claim>
            { 
                new Claim(ClaimTypes.Email, Guid.NewGuid().ToString())
            };

            var claimIdentity = new ClaimsIdentity(
                claim, CookieAuthenticationDefaults.AuthenticationScheme);
            var authPopirties = new AuthenticationProperties();

         await   HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimIdentity),
                authPopirties);





            return RedirectToAction("Index");
        }
        [AllowAnonymous]

        public IActionResult Logout() => View();


        public IActionResult Revoke() => View();
    }
}