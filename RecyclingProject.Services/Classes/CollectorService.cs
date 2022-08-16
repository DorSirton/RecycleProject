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
    public class CollectorService : ICollectorService
    {
        private readonly ICollectorRepository _repo;

        public CollectorService(ICollectorRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> AddBottles(ICollection<Bottle> bottleList, int collectorId)
        {
            if (bottleList.Count > 0)
            {
                await _repo.AddBottles(bottleList, collectorId);
                return true;
            }
            return false;
        }

        public IQueryable<Collector> GetAllCollectors()
        {
            return _repo.GetAllCollectors();
        }

        public Task<Collector?> GetCollectorDetails(int collectorId)
        {
            return _repo.GetCollectorDetails(collectorId);
        }

        public Task<IQueryable<Bottle>?> GetCollectorsBottles(int collectorId)
        {
            return _repo.GetCollectorsBottles(collectorId);
        }

        public void RemoveBottles(int collectorId)
        {
            _repo.RemoveBottles(collectorId);
        }

        public IQueryable<BottleCategory> GetAllBottleCategories()
        {
            return _repo.GetAllBottleCategories();
        }

        public async Task<double?> GetBottleCollectionWorth(int collectorId)
        {
            var sum = await _repo.GetBottleCollectionWorth(collectorId);
            return sum;
        }

    }
}
