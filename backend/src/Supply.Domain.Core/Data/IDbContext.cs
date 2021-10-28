using Microsoft.EntityFrameworkCore;

namespace Supply.Domain.Core.Data
{
    public interface IDbContext : IUnitOfWork
    {
        DbSet<T> Set<T>() where T : class;
    }
}
