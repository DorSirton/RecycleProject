using Microsoft.AspNetCore.Mvc;
using RecyclingProject.Client.Models;
using RecyclingProject.Data.Model;
using RecyclingProject.Services.Interfaces;

namespace RecyclingProject.Client.Controllers
{
    public class CollectorController : Controller
    {
        private readonly ICollectorService _service;
        private readonly IAddressService _addressService;

        public CollectorController(ICollectorService service, IAddressService addressService)
        {
            _service = service;
            _addressService = addressService;
        }

        public async Task<IActionResult> CollectorDetails(int collectorId)
        {
            var collector = await _service.GetCollectorDetails(collectorId);
            var address = await _addressService.GetAddress(collector.AddressId);
            var country = await _addressService.GetCountry(address.CityId);
            var currentWorth = await _service.GetBottleCollectionWorth(collector.Id);
            ViewBag.Country = country.Name;
            ViewBag.City = address.City.Name;
            ViewBag.Street = address.Address1;
            ViewBag.CurrentWorth = currentWorth;
            return View(collector);
        }

        [HttpGet]
        public IActionResult AddBottles(int collectorId)
        {
            var categories = _service.GetAllBottleCategories();
            ViewBag.CollectorId = collectorId;
            return View(categories);
        }

        [HttpPost]
        public async Task<IActionResult> AddBottles(int collectorId, int categoryId, int numOfBottles)
        {
            ICollection<Bottle> collection = new List<Bottle>();
            for (int i = 0; i < numOfBottles; i++)
            {
                collection.Add(new Bottle { CategoryId = categoryId });
            }
            await _service.AddBottles(collection, collectorId);
            return RedirectToAction("CollectorDetails", new { collectorId = collectorId });
        }

    }
}
