using RecyclingProject.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclingProject.Data.Repositories.Interfaces
{
    public interface ICollectorRepository
    {
        Task<IQueryable<Bottle>?> GetCollectorsBottles(int collectorId);

        Task<bool> AddBottles(ICollection<Bottle> bottleList, int collectorId);

        Task<bool> RemoveBottles(int collectorId);

        Task<Collector?> GetCollectorDetails(int collectorId);

        IQueryable<Collector?> GetAllCollectors();

        IQueryable<BottleCategory> GetAllBottleCategories();

        Task<double?> GetBottleCollectionWorth(int collectorId);
    }
}
