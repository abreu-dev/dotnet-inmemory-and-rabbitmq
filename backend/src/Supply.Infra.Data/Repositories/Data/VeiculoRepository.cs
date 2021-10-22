using Microsoft.EntityFrameworkCore;
using Supply.Domain.Core.Messaging.Data;
using Supply.Domain.Entities;
using Supply.Domain.Interfaces;
using Supply.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Supply.Infra.Data.Repositories.Data
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly SupplyDataContext _supplyContext;

        public VeiculoRepository(SupplyDataContext supplyContext)
        {
            _supplyContext = supplyContext;
        }

        public IUnitOfWork UnitOfWork => _supplyContext;

        private IQueryable<Veiculo> Query()
        {
            return _supplyContext.Veiculo.AsNoTracking().Where(x => !x.Removed);
        }

        public async Task<IEnumerable<Veiculo>> GetAll()
        {
            return await Query().ToListAsync();
        }

        public async Task<IEnumerable<Veiculo>> Search(Expression<Func<Veiculo, bool>> predicate)
        {
            return await Query().Where(predicate).ToListAsync();
        }

        public async Task<Veiculo> GetById(Guid id)
        {
            return await Query().FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Add(Veiculo veiculo)
        {
            _supplyContext.Veiculo.Add(veiculo);
        }

        public void Update(Veiculo veiculo)
        {
            _supplyContext.Veiculo.Update(veiculo);
        }

        public void Remove(Veiculo veiculo)
        {
            veiculo.Remove();
            _supplyContext.Veiculo.Update(veiculo);
        }
    }
}
