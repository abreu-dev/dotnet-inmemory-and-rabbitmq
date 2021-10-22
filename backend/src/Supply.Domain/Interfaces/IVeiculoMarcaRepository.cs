using Supply.Domain.Core.Messaging.Data;
using Supply.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Supply.Domain.Interfaces
{
    public interface IVeiculoMarcaRepository
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<VeiculoMarca>> GetAll();
        Task<IEnumerable<VeiculoMarca>> Search(Expression<Func<VeiculoMarca, bool>> predicate);
        Task<VeiculoMarca> GetById(Guid id);

        void Add(VeiculoMarca veiculoMarca);
        void Update(VeiculoMarca veiculoMarca);
        void Remove(VeiculoMarca veiculoMarca);
    }
}
