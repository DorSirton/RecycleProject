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
    public class RecyclerService : IRecyclerService
    {
        private readonly IRecyclerRepository _repo;

        public RecyclerService(IRecyclerRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> AddBottles(int collectorId, int recyclerId)
        {
            return await _repo.AddBottles(collectorId, recyclerId);
        }

        public IQueryable<BottleCategory> GetAllBottleCategories()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Collector> GetAllCollectors(Recycler recycler)
        {
            return _repo.GetCollectingPoints(recycler);
        }

        public IQueryable<Recycler> GetAllRecyclers()
        {
            return _repo.GetAllRecyclers();
        }

        public async Task<double?> GetBottleCollectionWorth(int recyclerId)
        {
            return await _repo.GetBottleCollectionWorth(recyclerId);
        }

        public Task<IQueryable<Bottle>?> GetRecyclerBottles(int recyclerId)
        {
            throw new NotImplementedException();
        }

        public async Task<Recycler?> GetRecyclerDetails(int collectorId)
        {
            return await _repo.GetRecyclerDetails(collectorId);
        }

        public async Task<Collector?> GetCollectorDetails(int collectorId)
        {
            var collectors = GetAllCollectors();
            return collectors.FirstOrDefault(c => c.Id == collectorId);
        }

        private IQueryable<Collector> GetAllCollectors()
        {
            return _repo.GetAllCollectors();
        }

        public async Task<double?> CashOut(int recyclerId)
        {
            var res = await _repo.CashOut(recyclerId);
            if (res == 0 || res == null) return null;
            return res;
        }
    }
}
