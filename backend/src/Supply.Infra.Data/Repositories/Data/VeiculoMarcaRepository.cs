using Microsoft.EntityFrameworkCore;
using Supply.Domain.Core.Data;
using Supply.Domain.Entities;
using Supply.Domain.Interfaces;
using Supply.Infra.Data.Context;
using System.Linq;

namespace Supply.Infra.Data.Repositories.Data
{
    public class VeiculoMarcaRepository : Repository<VeiculoMarca>, IVeiculoMarcaRepository
    {
        public VeiculoMarcaRepository(SupplyDataContext supplyContext) : base(supplyContext) { }

        protected override IQueryable<VeiculoMarca> IncludeQuery()
        {
            return Query().Include(x => x.VeiculoModelos.Where(w => !w.Removed));
        }
    }
}
