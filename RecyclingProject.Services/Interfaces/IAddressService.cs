using RecyclingProject.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclingProject.Services.Interfaces
{
    public interface IAddressService 
    {
        Task<Address?> GetAddress(int addressId);

        IQueryable<Address> GetAllAddresses();

        Task<City?> GetCity(int cityId);

        Task<Country?> GetCountry(int cityId);
    }
}
