using Supply.Domain.Core.Messaging.Data;
using Supply.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Supply.Domain.Interfaces
{
    public interface IVeiculoRepository
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<Veiculo>> GetAll();
        Task<IEnumerable<Veiculo>> Search(Expression<Func<Veiculo, bool>> predicate);
        Task<Veiculo> GetById(Guid id);

        void Add(Veiculo veiculo);
        void Update(Veiculo veiculo);
        void Remove(Veiculo veiculo);
    }
}
