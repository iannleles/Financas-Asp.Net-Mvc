using Financas.Domain.Contracts.Repositories.Common;
using Financas.Domain.Entities;

namespace Financas.Domain.Contracts.Repositories
{
    public interface IDespesaRepository : IGenericRepository<Despesa>
    {
        bool CategoriasNaoVinculadas(int? categoriaId);
        bool DescricaoNaoCadastrado(int? id, string nome);
    }
}
