

using Financas.Domain.Dtos;

namespace Financas.Domain.Contracts.Services
{
   public interface IAutenticacaoAppService
    {
       UsuarioDTO AutenticarUsuario(string _email, string _senha);

    }
}





