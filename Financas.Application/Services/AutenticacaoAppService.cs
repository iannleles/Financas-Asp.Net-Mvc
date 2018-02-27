using System;
using System.Web;
using Financas.Domain.Contracts.Services;
using Financas.Domain.Dtos;


namespace Financas.Application.Services
{
    public class AutenticacaoAppService : IAutenticacaoAppService
    {
        private readonly IUsuarioAppService _UsuarioAppService;

        public AutenticacaoAppService(IUsuarioAppService UsuarioAppService)
        {
            _UsuarioAppService = UsuarioAppService;
        }

        public UsuarioDTO AutenticarUsuario(string _email, string _senha)
        {

            try
            {
                string senhaCriptografa = CriptografiaAppService.Criptografar(_senha);
                return _UsuarioAppService.GetByEmailSenha(_email, senhaCriptografa);

            }
            catch (Exception ex)
            {
                // todo: logar
                throw;
            }
        }



    }
}
