using Financas.Domain.Contracts.Repositories.Common;
using Financas.Domain.Dtos;
using Financas.Domain.Entities;

namespace Financas.Domain.Contracts.Repositories
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        UsuarioDTO ObterUsuarioPorEmailSenha(string email, string senha);
    }
}
