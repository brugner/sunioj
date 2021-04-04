using Sunioj.Core.Contracts.Repositories;
using System.Threading.Tasks;

namespace Sunioj.Core.Contracts.UnitsOfWork
{
    public interface IUnitOfWork
    {
        IUsersRepository Users { get; }
        IPostsRepository Posts { get; }
        IMessagesRepository Messages { get; }
        IServicePackagesRepository ServicePackages { get; }
        IServiceFaqsRepository ServiceFaqs { get; }
        ISettingsRepository Settings { get; }
        IResumesRepository Resumes { get; }

        Task<int> CompleteAsync();
    }
}
