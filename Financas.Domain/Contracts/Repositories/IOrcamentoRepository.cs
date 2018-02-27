using Financas.Domain.Contracts.Repositories.Common;
using Financas.Domain.Entities;

namespace Financas.Domain.Contracts.Repositories
{
    public interface IOrcamentoRepository : IGenericRepository<Orcamento>
    {
        bool CategoriasNaoVinculadas(int? categoriaId);
    }
}
