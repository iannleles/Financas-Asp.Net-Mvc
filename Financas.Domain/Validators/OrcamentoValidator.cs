using Financas.Domain.Contracts.Repositories;
using Financas.Domain.Dtos;
using Financas.Domain.Resources;
using FluentValidation;

namespace Financas.Domain.Entidade.Validators
{
    public class OrcamentoValidator : AbstractValidator<OrcamentoDTO>
    {
        //Atributo

        //Construtor
        public OrcamentoValidator()
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

        public void ValidarCadastro(OrcamentoDTO dto)
        {
            this.ValidateAndThrow(dto, "Cadastro");
        }

        public void ValidarEdicao(OrcamentoDTO dto)
        {
            this.ValidateAndThrow(dto, "Edicao");
        }

        public void ValidarExclusao(OrcamentoDTO dto)
        {
            this.ValidateAndThrow(dto, "Exclusao");
        }

        #endregion

    }
}


