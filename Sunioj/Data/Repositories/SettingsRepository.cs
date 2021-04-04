using Microsoft.EntityFrameworkCore;
using Sunioj.Core.Contracts.Repositories;
using Sunioj.Data.Entities;
using System.Threading.Tasks;

namespace Sunioj.Data.Repositories
{
    public class SettingsRepository : Repository<Setting, int>, ISettingsRepository
    {
        public SettingsRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<Setting> GetByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            return await AppDbContext.Settings.SingleOrDefaultAsync(x => x.Name.Equals(name.ToLower()));
        }
    }
}
