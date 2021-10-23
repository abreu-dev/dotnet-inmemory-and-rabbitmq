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
    public class VeiculoModeloRepository : IVeiculoModeloRepository
    {
        private readonly SupplyDataContext _supplyContext;

        public VeiculoModeloRepository(SupplyDataContext supplyContext)
        {
            _supplyContext = supplyContext;
        }

        public IUnitOfWork UnitOfWork => _supplyContext;

        private IQueryable<VeiculoModelo> Query()
        {
            return _supplyContext.VeiculoModelo.AsNoTracking().Where(x => !x.Removed);
        }

        private IQueryable<VeiculoModelo> IncludeQuery()
        {
            return Query().Include(x => x.VeiculoMarca);
        }

        public async Task<IEnumerable<VeiculoModelo>> GetAll()
        {
            return await Query().ToListAsync();
        }

        public async Task<IEnumerable<VeiculoModelo>> Search(Expression<Func<VeiculoModelo, bool>> predicate)
        {
            return await Query().Where(predicate).ToListAsync();
        }

        public async Task<VeiculoModelo> GetById(Guid id)
        {
            return await Query().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<VeiculoModelo> GetByIdWithIncludes(Guid id)
        {
            return await IncludeQuery().FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Add(VeiculoModelo veiculoModelo)
        {
            _supplyContext.VeiculoModelo.Add(veiculoModelo);
        }

        public void Update(VeiculoModelo veiculoModelo)
        {
            _supplyContext.VeiculoModelo.Update(veiculoModelo);
        }

        public void Remove(VeiculoModelo veiculoModelo)
        {
            veiculoModelo.Remove();
            _supplyContext.VeiculoModelo.Update(veiculoModelo);
        }
    }
}
