using System.Threading.Tasks;

namespace TBCTest.Data
{
    /// <summary>
    /// Commits all pending changes to the database in one transaction.
    /// </summary>
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
