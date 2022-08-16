using RecyclingProject.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclingProject.Services.Interfaces
{
    public interface IRecyclerService
    {
        Task<Recycler?> GetRecyclerDetails(int collectorId);
        IQueryable<Recycler> GetAllRecyclers();

        Task<bool> AddBottles(int collectorId, int recyclerId);

        Task<IQueryable<Bottle>?> GetRecyclerBottles(int recyclerId);

        Task<double?> CashOut(int recyclerId);

        IQueryable<BottleCategory> GetAllBottleCategories();

        IQueryable<Collector> GetAllCollectors(Recycler recycler);

        Task<double?> GetBottleCollectionWorth(int recyclerId);

        Task<Collector?> GetCollectorDetails(int collectorId);
    }
}
