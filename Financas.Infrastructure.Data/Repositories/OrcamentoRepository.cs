using Financas.Domain.Common.Specifications;
using Financas.Domain.Contracts.Repositories;
using Financas.Domain.Entities;
using Financas.Domain.Specs.OrcamentoSpecs;
using Financas.Infrastructure.Data.Contracts;
using Financas.Infrastructure.Data.Repositories.Common;

namespace Financas.Infrastructure.Data.Repositories
{
    public class OrcamentoRepository : GenericRepository<Orcamento>, IOrcamentoRepository
    {
        public OrcamentoRepository(IDbContext context)
            : base(context)
        {

        }

        public bool CategoriasNaoVinculadas(int? categoriaId)
        {
            ISpecification<Orcamento> criterio;

            criterio = new OrcamentoPorIdCategoria(categoriaId);

            return NotExists(criterio);


        }


    }
}
