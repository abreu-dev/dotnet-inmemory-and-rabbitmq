using Supply.Domain.Core.Messaging.Data;
using Supply.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Supply.Domain.Interfaces
{
    public interface IVeiculoModeloRepository
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<VeiculoModelo>> GetAll();
        Task<IEnumerable<VeiculoModelo>> Search(Expression<Func<VeiculoModelo, bool>> predicate);
        Task<VeiculoModelo> GetById(Guid id);
        Task<VeiculoModelo> GetByIdWithIncludes(Guid id);

        void Add(VeiculoModelo veiculoModelo);
        void Update(VeiculoModelo veiculoModelo);
        void Remove(VeiculoModelo veiculoModelo);
    }
}
