using Microsoft.EntityFrameworkCore;
using Supply.Domain.Core.Data;
using Supply.Domain.Entities;
using Supply.Domain.Interfaces;
using Supply.Infra.Data.Context;
using System.Linq;

namespace Supply.Infra.Data.Repositories.Data
{
    public class VeiculoModeloRepository : Repository<VeiculoModelo>, IVeiculoModeloRepository
    {
        public VeiculoModeloRepository(SupplyDataContext supplyContext) : base(supplyContext) { }

        protected override IQueryable<VeiculoModelo> IncludeQuery()
        {
            return Query()
                .Include(x => x.VeiculoMarca)
                .Include(x => x.Veiculos.Where(w => !w.Removed));
        }
    }
}
