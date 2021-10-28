using Microsoft.EntityFrameworkCore;
using Supply.Domain.Core.Data;
using Supply.Domain.Entities;
using Supply.Domain.Interfaces;
using Supply.Infra.Data.Context;
using System.Linq;

namespace Supply.Infra.Data.Repositories.Data
{
    public class VeiculoRepository : Repository<Veiculo>, IVeiculoRepository
    {
        public VeiculoRepository(SupplyDataContext supplyContext) : base(supplyContext) { }

        protected override IQueryable<Veiculo> IncludeQuery()
        {
            return Query()
                .Include(x => x.VeiculoModelo)
                .ThenInclude(x => x.VeiculoMarca);
        }
    }
}
