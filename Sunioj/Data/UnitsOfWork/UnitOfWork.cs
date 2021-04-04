using Sunioj.Core.Contracts.Repositories;
using Sunioj.Core.Contracts.UnitsOfWork;
using System.Threading.Tasks;

namespace Sunioj.Data.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IUsersRepository Users { get; private set; }
        public IPostsRepository Posts { get; private set; }
        public IMessagesRepository Messages { get; private set; }
        public IServicePackagesRepository ServicePackages { get; private set; }
        public IServiceFaqsRepository ServiceFaqs { get; private set; }
        public ISettingsRepository Settings { get; private set; }
        public IResumesRepository Resumes { get; private set; }

        public UnitOfWork(AppDbContext context,
            IUsersRepository users,
            IPostsRepository roles,
            IMessagesRepository messages,
            IServicePackagesRepository servicePackages,
            IServiceFaqsRepository serviceFaqs,
            ISettingsRepository settings,
            IResumesRepository resumes)
        {
            _context = context;

            Users = users;
            Posts = roles;
            Messages = messages;
            ServicePackages = servicePackages;
            ServiceFaqs = serviceFaqs;
            Settings = settings;
            Resumes = resumes;
        }

        public Task<int> CompleteAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
