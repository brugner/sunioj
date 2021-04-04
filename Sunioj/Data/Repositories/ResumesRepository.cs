using Microsoft.EntityFrameworkCore;
using Sunioj.Core.Contracts.Repositories;
using Sunioj.Data.Entities;
using System.Threading.Tasks;

namespace Sunioj.Data.Repositories
{
    public class ResumesRepository : Repository<Resume, int>, IResumesRepository
    {
        public ResumesRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<Resume> GetAsync()
        {
            return await AppDbContext.Resumes
                .Include(x => x.Experience)
                .Include(x => x.Education)
                .Include(x => x.Courses)
                .Include(x => x.Skills)
                .Include(x => x.Languages)
                .Include(x => x.Interests)
                .FirstOrDefaultAsync();
        }
    }
}
