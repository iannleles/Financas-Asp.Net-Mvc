using System;
using Financas.Domain.Dtos;
using Financas.Domain.Entities;


namespace Financas.Domain.Contracts.Services
{
    public interface IUsuarioAppService : IDisposable
    {
        Usuario GetById(int Id);
        UsuarioDTO GetByEmailSenha(string _email, string _senha);
    }
}
