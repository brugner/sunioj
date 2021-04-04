using Sunioj.Core.Contracts.Repositories;
using Sunioj.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunioj.Data.Repositories
{
    public class MessagesRepository : Repository<Message, int>, IMessagesRepository
    {
        public MessagesRepository(AppDbContext context) : base(context)
        {

        }

        public async override Task<IEnumerable<Message>> GetAllAsync()
        {
            var messages = await base.GetAllAsync();

            return messages.OrderByDescending(x => x.CreatedAt);
        }
    }
}
