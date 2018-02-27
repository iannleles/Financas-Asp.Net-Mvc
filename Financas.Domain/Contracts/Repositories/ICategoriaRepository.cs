using Financas.Domain.Contracts.Repositories.Common;
using Financas.Domain.Entities;

namespace Financas.Domain.Contracts.Repositories
{
    public interface ICategoriaRepository : IGenericRepository<Categoria>
    {
        bool NomeNaoCadastrado(int? id, string nome, int? usuarioId, string tipo);

        bool UsuarioCadastroValido(int? id, int? usuarioId);

        bool IdCategoriaExiste(int? id);

        bool CategoriasPaiNaoVinculadas(int? idCategoriaPai);

    }
}
