using RecyclingProject.Data.Model;
using RecyclingProject.Data.Repositories.Interfaces;
using RecyclingProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclingProject.Services.Classes
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _repo;

        public AddressService(IAddressRepository repo)
        {
            _repo = repo;
        }
        public async Task<Address?> GetAddress(int addressId)
        {
            var res = await _repo.GetAddress(addressId);
            if (res == null) return null;
            return res;
        }

        public IQueryable<Address> GetAllAddresses()
        {
            return _repo.GetAllAddresses();
        }
        public async Task<City?> GetCity(int cityId)
        {
            return await _repo.GetCity(cityId);
        }

        public async Task<Country?> GetCountry(int cityId)
        {
            return await _repo.GetCountry(cityId);
        }
    }
}
