using Microsoft.AspNetCore.Mvc;
using RecyclingProject.Data.Model;
using RecyclingProject.Services.Interfaces;

namespace RecyclingProject.Client.Controllers
{
    public class RecyclerController : Controller
    {
        private readonly IRecyclerService _service;
        private readonly IAddressService _addressService;

        public RecyclerController(IRecyclerService service, IAddressService addressService)
        {
            _service = service;
            _addressService = addressService;
        }
        public async Task<IActionResult> RecyclerDetails(int recyclerId)
        {
            var recycler = await _service.GetRecyclerDetails(recyclerId);
            var address = await _addressService.GetAddress(recycler.AddressId);
            var country = await _addressService.GetCountry(address.CityId);
            var currentWorth = await _service.GetBottleCollectionWorth(recycler.Id);
            ViewBag.Country = country.Name;
            ViewBag.City = address.City.Name;
            ViewBag.Street = address.Address1;
            ViewBag.CurrentWorth = currentWorth;
            return View(recycler);
        }
        public async Task<IActionResult> GoCollect(int recyclerId)
        {
            var recycler = await _service.GetRecyclerDetails(recyclerId);
            if (recycler == null) return NotFound();
            ViewBag.RecyclerId = recyclerId;
            var collectionPoints = _service.GetAllCollectors(recycler);
            if (!collectionPoints.Any()) return RedirectToAction("NoAvailableCollectingPoints", "Error", new { recyclerId = recyclerId });
            return View(collectionPoints);
        }
        public async Task<IActionResult> SeeLocation(int collectorId,int recyclerId)
        {
            ViewBag.recyclerId = recyclerId;
            var collector = await _service.GetCollectorDetails(collectorId);
            if (collector == null) return NotFound();
            return View(collector);
        }
        public async Task<IActionResult> CashOut(int recyclerId)
        {
            var recycler = await _service.GetRecyclerDetails(recyclerId);
            var result = await _service.CashOut(recycler.Id);
            if (result == null || result == 0) return RedirectToAction("NoValueToCash", "Error", new { recyclerId = recyclerId });
            return RedirectToAction("Congratulations", new { result = result, recyclerId = recyclerId });
        }

        public async Task<IActionResult> Redeem(int collectorId, int recyclerId)
        {
            var res = await _service.AddBottles(collectorId, recyclerId);
            if (!res) return NotFound();
            return RedirectToAction("RecyclerDetails", new { recyclerId = recyclerId });
        }
        public IActionResult Congratulations(double result, int recyclerId)
        {
            ViewBag.Result = result;
            ViewBag.Id = recyclerId;
            return View();
        }
    }
}
