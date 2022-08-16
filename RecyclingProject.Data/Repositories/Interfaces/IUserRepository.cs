﻿using RecyclingProject.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclingProject.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<Country> GetCountries();
        IQueryable<City> GetCities(int countryId);
        IQueryable<Address>  GetStreets(int cityId);
        Task<Collector> AddCollector(Collector c);
        Task<Recycler> AddRecycler(Recycler r);
    }
}
