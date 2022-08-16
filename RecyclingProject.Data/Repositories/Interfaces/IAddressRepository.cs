using RecyclingProject.Data.Model;
using System.Linq;

namespace RecyclingProject.Data.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        Task<Address?> GetAddress(int Id);

        IQueryable<Address> GetAllAddresses();

        Task<City?> GetCity(int cityId);

        Task<Country?> GetCountry(int cityId);
    }
}