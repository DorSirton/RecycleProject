using Microsoft.EntityFrameworkCore;
using RecyclingProject.Data.Contexts;
using RecyclingProject.Data.Model;
using RecyclingProject.Data.Repositories.Interfaces;

namespace RecyclingProject.Data.Repositories.Classes
{
    public class AddressRepository : IAddressRepository
    {
        private readonly RecycleProjectDataContext _context;
        public AddressRepository(RecycleProjectDataContext context)
        {
            _context = context;
        }

        public async Task<Address?> GetAddress(int Id)
        {
            var addresses = GetAllAddresses();
            var address = await addresses.FirstOrDefaultAsync(a => a.Id == Id);
            return address;
        }

        public IQueryable<Address> GetAllAddresses()
        {
            return _context.Addresses
                .Include(m => m.City);
        }

        public async Task<City?> GetCity(int cityId)
        {
            return await _context.Cities.FirstOrDefaultAsync(c=>c.Id == cityId);
        }
        public async Task<Country?> GetCountry(int cityId)
        {
            var city = await GetCity(cityId);
            if (city == null) return null;
            return await _context.Countries.FirstOrDefaultAsync(c => c.Id == city.CountryId);
        }
    }
}
