using Financas.Domain.Common.Specifications;
using Financas.Domain.Contracts.Repositories;
using Financas.Domain.Entities;
using Financas.Domain.Specs.DespesaSpecs;
using Financas.Infrastructure.Data.Contracts;
using Financas.Infrastructure.Data.Repositories.Common;

namespace Financas.Infrastructure.Data.Repositories
{
    public class DespesaRepository : GenericRepository<Despesa>, IDespesaRepository
    {
        public DespesaRepository(IDbContext context)
            : base(context)
        {

        }

        public bool CategoriasNaoVinculadas(int? categoriaId)
        {
            ISpecification<Despesa> criterio;

            criterio = new DespesaPorIdCategoria(categoriaId);

            return NotExists(criterio);

        }

        public bool DescricaoNaoCadastrado(int? id, string nome)
        {
            ISpecification<Despesa> criterio;

            criterio = new DespesaPorDescricaoExato(nome);
            criterio = criterio.And(new DespesaPorPorIdDiferente(id));

            return NotExists(criterio);

        }
    }
}
