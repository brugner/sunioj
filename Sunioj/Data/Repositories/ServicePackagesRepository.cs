using Sunioj.Core.Contracts.Repositories;
using Sunioj.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunioj.Data.Repositories
{
    public class ServicePackagesRepository : Repository<ServicePackage, int>, IServicePackagesRepository
    {
        public ServicePackagesRepository(AppDbContext context) : base(context)
        {

        }

        public async override Task<IEnumerable<ServicePackage>> GetAllAsync()
        {
            var messages = await base.GetAllAsync();

            return messages.OrderBy(x => x.Order);
        }
    }
}
