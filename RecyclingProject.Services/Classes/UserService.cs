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
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repository)
        {
            _repo = repository;
        }

        public IQueryable<Country> GetCountries()
        {
            return _repo.GetCountries();
        }

        public IQueryable<City> GetCities(int countryId)
        {
            return _repo.GetCities(countryId);
        }

        public IQueryable<Address> GetStreets(int cityId)
        {
            return _repo.GetStreets(cityId);
        }

        public async Task<Collector> AddCollector(Collector c)
        {
            return await _repo.AddCollector(c);
        }

        public async Task<Recycler> AddRecycler(Recycler r)
        {
           return await _repo.AddRecycler(r);
        }
    }
}
