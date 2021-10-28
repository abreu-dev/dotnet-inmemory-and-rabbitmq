using System.Threading.Tasks;

namespace Supply.Domain.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
