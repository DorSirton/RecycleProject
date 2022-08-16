using RecyclingProject.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclingProject.Data.Repositories.Interfaces
{
    public interface IRecyclerRepository
    {
        IQueryable<Recycler?> GetAllRecyclers();

        Task<IQueryable<Bottle>?> GetRecyclerBottles(int recyclerId);

        Task<bool> AddBottles(int collectorId, int recyclerId);

        Task<bool> RemoveBottles(Recycler recycler);

        Task<Recycler?> GetRecyclerDetails(int collectorId);

        IQueryable<Collector> GetCollectingPoints(Recycler recycler);

        void DepositBottles(Recycler recycler);

        Task<double?> GetBottleCollectionWorth(int recyclerId);

        Task<double?> CashOut(int recyclerId);

        IQueryable<Collector> GetAllCollectors();
    }
}
