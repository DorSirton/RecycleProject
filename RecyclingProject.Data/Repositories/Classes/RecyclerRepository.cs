using Microsoft.EntityFrameworkCore;
using RecyclingProject.Data.Contexts;
using RecyclingProject.Data.Model;
using RecyclingProject.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclingProject.Data.Repositories.Classes
{
    public class RecyclerRepository : IRecyclerRepository
    {
        RecycleProjectDataContext _context;
        public RecyclerRepository(RecycleProjectDataContext context)
        {
            _context = context;
        }
        public async Task<bool> AddBottles(int collectorId, int recyclerId)
        {
            var collector = await GetCollectorDetails(collectorId);
            var recycler = await GetRecyclerDetails(recyclerId);
            var categories = _context.BottleCategories;
            if (recycler != null && collector != null)
            {
                foreach (var removedBottle in collector.Bottles)
                {
                    collector.Bottles.Remove(removedBottle);
                    //removedBottle.CollectorId = null;
                    recycler.Bottles.Add(removedBottle);
                    foreach (var category in categories)
                    {
                        if (category.Id == removedBottle.CategoryId)
                        {
                            recycler.CurrentWorth += removedBottle.Category.RecyclePrice;
                        }
                    }
                    recycler.CurrentNumOfBottles++;
                    recycler.NumOfCollectedPoints++;
                }
                collector.CurrentNumOfBottles = 0;
                collector.CurrentWorth = 0;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        private async Task<Collector?> GetCollectorDetails(int collectorId)
        {
            var collectors = GetAllCollectors();
            if (collectors == null) return null;
            return await collectors.FirstOrDefaultAsync(c => c.Id == collectorId);
        }

        public IQueryable<Collector> GetAllCollectors()
        {
            return _context.Collectors
                .Include(c => c.Bottles)
                .Include(c => c.Address);
        }

        public void DepositBottles(Recycler recycler)
        {
            recycler.TotalWorthHistory += recycler.CurrentWorth;
            RemoveBottles(recycler);
        }

        public IQueryable<Recycler?> GetAllRecyclers()
        {
            return _context.Recyclers
                .Include(c => c.Address)
                .Include(c => c.Bottles);
        }

        public IQueryable<Collector> GetCollectingPoints(Recycler recycler)
        {
            var list = _context.Collectors.Where(c => c.Address.CityId == recycler.Address.CityId && c.CurrentNumOfBottles > 0);
            return list;
        }

        public async Task<IQueryable<Bottle>?> GetRecyclerBottles(int recyclerId)
        {
            var recycler = await GetRecyclerDetails(recyclerId);
            if (recycler != null) return (IQueryable<Bottle>?)recycler.Bottles;
            return null;
        }

        public async Task<Recycler?> GetRecyclerDetails(int collectorId)
        {
            var recyclers = GetAllRecyclers();
            return await recyclers.FirstOrDefaultAsync(c => c!.Id == collectorId);
        }

        public async Task<bool> RemoveBottles(Recycler recycler)
        {
            if (recycler == null) return false;
            foreach (var bottle in recycler.Bottles)
            {
                recycler.Bottles.Remove(bottle);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<double?> CashOut(int recyclerId)
        {
            var recycler = await GetRecyclerDetails(recyclerId);
            if (recycler == null) return null;
            await RemoveBottles(recycler);
            var sum = recycler.CurrentWorth;
            recycler.TotalHistoryNumOfBottles += recycler.CurrentNumOfBottles;
            recycler.CurrentNumOfBottles = 0;
            recycler.TotalWorthHistory += recycler.CurrentWorth;
            recycler.CurrentWorth = 0;
            await _context.SaveChangesAsync();
            return sum;
        }

        public async Task<double?> GetBottleCollectionWorth(int recyclerId)
        {
            var recycler = await GetRecyclerDetails(recyclerId);
            return recycler.CurrentWorth;
        }


    }
}
