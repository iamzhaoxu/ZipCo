using System.Threading;
using System.Threading.Tasks;

namespace ZipCo.Users.Application.Interfaces
{
    public interface IUserUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        void UndoChange();

    }
}
