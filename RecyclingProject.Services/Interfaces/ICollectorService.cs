using RecyclingProject.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclingProject.Services.Interfaces
{
    public interface ICollectorService 
    {
        IQueryable<Collector> GetAllCollectors();

        Task<bool> AddBottles(ICollection<Bottle> bottleList, int collectorId);

        Task<Collector?> GetCollectorDetails(int collectorId);

        Task<IQueryable<Bottle>?> GetCollectorsBottles(int collectorId);

        void RemoveBottles(int collectorId);

        IQueryable<BottleCategory> GetAllBottleCategories();
        Task<double?> GetBottleCollectionWorth(int collectorId);

    }
}
