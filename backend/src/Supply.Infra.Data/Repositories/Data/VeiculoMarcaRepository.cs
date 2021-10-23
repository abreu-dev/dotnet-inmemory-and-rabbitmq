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
    public class VeiculoMarcaRepository : IVeiculoMarcaRepository
    {
        private readonly SupplyDataContext _supplyContext;

        public VeiculoMarcaRepository(SupplyDataContext supplyContext)
        {
            _supplyContext = supplyContext;
        }

        public IUnitOfWork UnitOfWork => _supplyContext;

        private IQueryable<VeiculoMarca> Query()
        {
            return _supplyContext.VeiculoMarca.AsNoTracking().Where(x => !x.Removed);
        }

        private IQueryable<VeiculoMarca> IncludeQuery()
        {
            return Query().Include(x => x.VeiculoModelos.Where(w => !w.Removed));
        }

        public async Task<IEnumerable<VeiculoMarca>> GetAll()
        {
            return await Query().ToListAsync();
        }

        public async Task<IEnumerable<VeiculoMarca>> Search(Expression<Func<VeiculoMarca, bool>> predicate)
        {
            return await Query().Where(predicate).ToListAsync();
        }

        public async Task<VeiculoMarca> GetById(Guid id)
        {
            return await Query().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<VeiculoMarca> GetByIdWithIncludes(Guid id)
        {
            return await IncludeQuery().FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Add(VeiculoMarca veiculoMarca)
        {
            _supplyContext.VeiculoMarca.Add(veiculoMarca);
        }

        public void Update(VeiculoMarca veiculoMarca)
        {
            _supplyContext.VeiculoMarca.Update(veiculoMarca);
        }

        public void Remove(VeiculoMarca veiculoMarca)
        {
            veiculoMarca.Remove();
            _supplyContext.VeiculoMarca.Update(veiculoMarca);
        }
    }
}
