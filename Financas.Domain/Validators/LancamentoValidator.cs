
using Financas.Domain.Contracts.Repositories;
using Financas.Domain.Dtos;
using Financas.Domain.Resources;
using FluentValidation;

namespace Financas.Domain.Entidade.Validators
{
    public class LancamentoValidator : AbstractValidator<LancamentoDTO>
    {
        //Atributo

        //Construtor
        public LancamentoValidator()
        {

            RuleSet("Cadastro", () =>
            {

            });

            RuleSet("Edicao", () =>
            {

            });

            RuleSet("Exclusao", () =>
            {
   
            });
 
        }

        #region Regras Simples

      
        #endregion

        #region Regras Complexas

        #endregion

        #region Operações de Validação de Domínio

        public void ValidarCadastro(LancamentoDTO dto)
        {
            this.ValidateAndThrow(dto, "Cadastro");
        }

        public void ValidarEdicao(LancamentoDTO dto)
        {
            this.ValidateAndThrow(dto, "Edicao");
        }

        public void ValidarExclusao(LancamentoDTO dto)
        {
            this.ValidateAndThrow(dto, "Exclusao");
        }

        #endregion

    }
}


