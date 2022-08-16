using RecyclingProject.Data.Contexts;
using RecyclingProject.Data.Model;
using RecyclingProject.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclingProject.Data.Repositories.Classes
{
    public class UserRepository : IUserRepository
    {
        RecycleProjectDataContext _context;
        public UserRepository(RecycleProjectDataContext context)
        {
            _context = context;
        }
        public IQueryable<Country> GetCountries()
        {
            return _context.Countries;
        }

        public IQueryable<City> GetCities(int countryId)
        {
            var cities = _context.Cities;
            var result = cities.Where(c => c.CountryId == countryId);
            return result;
        }

        public IQueryable<Address> GetStreets(int cityId)
        {
            var addresses = _context.Addresses;
            var result = addresses.Where(a => a.CityId == cityId);
            return result;
        }

        public async Task<Collector> AddCollector(Collector c)
        {
            var adrs = _context.Addresses.FirstOrDefault(a=>a.Id == c.Address.Id);
            c.Address = adrs;
            _context.Collectors.Add(c);
            await _context.SaveChangesAsync();
            return c;
        }

        public async Task<Recycler> AddRecycler(Recycler r)
        {
            var adrs = _context.Addresses.FirstOrDefault(a => a.Id == r.Address.Id);
            r.Address = adrs;
            _context.Recyclers.Add(r);
            await _context.SaveChangesAsync();
            return r;
        }
    }
}
