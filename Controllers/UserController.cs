using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecyclingProject.Client.Models;
using RecyclingProject.Data.Model;
using RecyclingProject.Services.Interfaces;
using System.Security.Claims;

namespace RecyclingProject.Client.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService _service;
        private readonly ICollectorService _collectorService;
        private readonly IRecyclerService _recyclerService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IUserService service, IRecyclerService recyclerService, ICollectorService collectorService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _service = service;
            _recyclerService = recyclerService;
            _collectorService = collectorService;
        }

        public IActionResult RegisterHomePage()
        {
            return View();
        }

        public IActionResult RegisterCollector()
        {
            ViewBag.Countries = new SelectList(_service.GetCountries(), "Id", "Name");
            return View();
        }

        public IActionResult RegisterRecycler()
        {
            ViewBag.Countries = new SelectList(_service.GetCountries(), "Id", "Name");
            return View();
        }

        public JsonResult LoadCities(int countryId)
        {
            var cities = _service.GetCities(countryId);
            return Json(new SelectList(cities, "Id", "Name"));
        }

        public JsonResult LoadStreets(int cityId)
        {
            var streets = _service.GetStreets(cityId);
            return Json(new SelectList(streets, "Id", "Address1"));
        }

        [HttpPost]
        public async Task<IActionResult> AddCollector([Bind("FirstName, LastName, Tel, Address")] Collector collector, string password, string username)
        {
            IdentityUser user = new IdentityUser { UserName = username };
            var result = await _userManager.CreateAsync(user, password);
            collector.Connection_Id = user.Id;
            if (result.Succeeded)
            {
                var newCollector = await _service.AddCollector(collector);
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("FailedCreatingUser", "Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRecycler([Bind("FirstName, LastName, Tel, Address")] Recycler recycler, string password, string username)
        {
            IdentityUser user = new IdentityUser { UserName = username };
            var result = await _userManager.CreateAsync(user, password);
            recycler.Connection_Id = user.Id;
            if (result.Succeeded)
            {
                var newRecycler = await _service.AddRecycler(recycler);
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("FailedCreatingUser", "Error");
            }
        }

        [HttpGet]
        public IActionResult Login() => View();
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Username);
                    var collectors = _collectorService.GetAllCollectors();
                    var recyclers = _recyclerService.GetAllRecyclers();

                    var collectorMatch = collectors.FirstOrDefault(u => u.Connection_Id == user.Id);
                    if (collectorMatch != null) return RedirectToAction("CollectorDetails", "Collector", new { collectorId = collectorMatch.Id });

                    var recyclerMatch = recyclers.FirstOrDefault(u => u.Connection_Id == user.Id);
                    if (recyclerMatch != null) return RedirectToAction("RecyclerDetails", "Recycler", new { recyclerId = recyclerMatch.Id });
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
