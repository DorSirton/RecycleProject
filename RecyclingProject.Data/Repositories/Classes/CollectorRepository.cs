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
    public class CollectorRepository : ICollectorRepository
    {
        private readonly RecycleProjectDataContext _context;
        public CollectorRepository(RecycleProjectDataContext context)
        {
            _context = context;
        }
        public IQueryable<Collector> GetAllCollectors()
        {
            return _context.Collectors
                .Include(c => c.Address)
                .Include(c => c.Bottles);
        }

        public async Task<bool> AddBottles(ICollection<Bottle> bottleList, int collectorId)
        {
            var collector = await GetCollectorDetails(collectorId);
            var categories = _context.BottleCategories;
            if (collector != null)
            {
                foreach (var bottle in bottleList)
                {
                    _context.Bottles.Add(bottle);
                    collector.Bottles.Add(bottle);
                    collector.CurrentNumOfBottles++;
                    collector.TotalHistoryNumOfBottles++;
                    foreach (var category in categories)
                    {
                        if (category.Id == bottle.CategoryId)
                        {
                            collector.TotalWorthHistory += bottle.Category.RecyclePrice;
                        }
                    }
                }
                var sum = await SetBottleCollectionWorth(collectorId);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Collector?> GetCollectorDetails(int collectorId)
        {
            var collectors = GetAllCollectors();
            var temp = await collectors.FirstOrDefaultAsync(c => c.Id == collectorId);
            return temp;
        }

        public async Task<IQueryable<Bottle>?> GetCollectorsBottles(int collectorId)
        {
            var collector = await GetCollectorDetails(collectorId);
            if (collector != null) return (IQueryable<Bottle>)collector.Bottles;
            return null;
        }

        public async Task<bool> RemoveBottles(int collectorId)
        {
            var collector = await GetCollectorDetails(collectorId);
            if (collector != null && collector.Bottles != null)
            {
                foreach (var bottle in collector.Bottles)
                {
                    collector.Bottles.Remove(bottle);
                }
                return true;
            }
            return false;
        }

        public IQueryable<BottleCategory> GetAllBottleCategories()
        {
            return _context.BottleCategories;
        }

        public async Task<double?> GetBottleCollectionWorth(int collectorId)
        {
            var collector = await GetCollectorDetails(collectorId);
            return collector.CurrentWorth;
        }

        private async Task<double?> SetBottleCollectionWorth(int collectorId)
        {
            var collector = await GetCollectorDetails(collectorId);
            if (collector == null) return null;
            double sum = 0;
            var categories = _context.BottleCategories;
            foreach (var category in categories)
            {
                foreach (var bottle in collector.Bottles)
                {
                    if (category.Id == bottle.CategoryId)
                    {
                        sum += category.RecyclePrice;
                    }
                }
            }
            collector.CurrentWorth = sum;
            await _context.SaveChangesAsync();
            return sum;
        }
    }
}
