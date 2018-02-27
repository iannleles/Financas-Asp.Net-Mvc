
using Financas.Domain.Contracts.Repositories;
using Financas.Domain.Dtos;
using Financas.Domain.Resources;
using FluentValidation;

namespace Financas.Domain.Entidade.Validators
{
    public class UsuarioValidator : AbstractValidator<UsuarioDTO>
    {

        //Construtor
        public UsuarioValidator()
        {

            RuleSet("Cadastro", () =>
            {
                NomeObrigatorio();
                EmailObrigatorio();
                SenhaObrigatorio();
                SaldoInicialObrigatorio();
                
            });

        }


        #region Regras Simples


        private void NomeObrigatorio()
        {
            RuleFor(e => e.Nome)
                .NotNull().WithMessage(UsuarioResources.NomeObrigatorio);
        }


        private void EmailObrigatorio()
        {
            RuleFor(e => e.Email)
                .NotNull().WithMessage(UsuarioResources.EmailObrigatorio);
        }

        private void SenhaObrigatorio()
        {
            RuleFor(e => e.Senha)
                .NotNull().WithMessage(UsuarioResources.SenhaObrigatorio);
        }

        private void SaldoInicialObrigatorio()
        {
            RuleFor(e => e.SaldoInicial)
                .NotNull().WithMessage(UsuarioResources.SaldoInicialObrigatorio);
        }


        #endregion

        #region Regras Complexas


        #endregion

        #region Operações de Validação de Domínio
        public void ValidarCadastro(UsuarioDTO dto)
        {
            this.ValidateAndThrow(dto, "Cadastro");
        }


        #endregion

     
    }
}


