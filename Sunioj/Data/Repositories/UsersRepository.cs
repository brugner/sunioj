using Microsoft.EntityFrameworkCore;
using Sunioj.Core.Contracts.Repositories;
using Sunioj.Data.Entities;
using System.Threading.Tasks;

namespace Sunioj.Data.Repositories
{
    public class UsersRepository : Repository<User, int>, IUsersRepository
    {
        public UsersRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<User> GetByEmailAsync(string email)
        {
            email = email.ToLower();

            return await AppDbContext.Users
                .SingleOrDefaultAsync(x => x.Email.Equals(email));
        }
    }
}
