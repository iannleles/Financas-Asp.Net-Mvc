using System.Linq;
using Financas.Domain.Common.Specifications;
using Financas.Domain.Contracts.Repositories;
using Financas.Domain.Contracts.Repositories.Common;
using Financas.Domain.Contracts.Services;
using Financas.Domain.Dtos;
using Financas.Domain.Entidade.Validators;
using Financas.Domain.Entities;
using Financas.Domain.Specs.UsuarioSpecs;

namespace Financas.Application.Services
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly UsuarioValidator usuarioValidator;


        public UsuarioAppService(
            IUnitOfWork unitOfWork,
            IUsuarioRepository usuarioRepository)
        {
            this.unitOfWork = unitOfWork;
            this.usuarioRepository = usuarioRepository;
            this.usuarioValidator = new UsuarioValidator();
        }

        public Usuario GetById(int id)
        {

            return usuarioRepository.GetById(id);


        }

        public UsuarioDTO GetByEmailSenha(string email, string senha)
        {
            return usuarioRepository.ObterUsuarioPorEmailSenha(email, senha);
        }


        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}




