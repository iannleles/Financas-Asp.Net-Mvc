using Financas.Domain.Contracts.Repositories;
using Financas.Domain.Dtos;
using Financas.Domain.Resources;
using FluentValidation;

namespace Financas.Domain.Entidade.Validators
{
    public class DespesaValidator : AbstractValidator<DespesaDTO>
    {
        //Atributo
        private readonly IDespesaRepository despesaRepository;

        //Construtor
        public DespesaValidator(
            IDespesaRepository despesaRepository)
        {
            this.despesaRepository = despesaRepository;

            RuleSet("Cadastro", () =>
            {
                UsuarioIdObrigatorio();
                DescricaoObrigatorio();
                ValorObrigatorio();
                VenctoObrigatorio();
                DescricaoTamanhoInvalido();
                DescricaoNaoDuplicado();
            });

            RuleSet("Edicao", () =>
            {
                IdObrigatorio();
                UsuarioIdObrigatorio();
                DescricaoObrigatorio();
                ValorObrigatorio();
                VenctoObrigatorio();
                DescricaoTamanhoInvalido();
            });

            RuleSet("Exclusao", () =>
            {
                IdObrigatorio();
            });
 
        }

        #region Regras Simples

        private void IdObrigatorio()
        {
            RuleFor(e => e.Id)
                .NotNull().WithMessage(DespesaResources.IdObrigatorio);
        }

        private void UsuarioIdObrigatorio()
        {
            RuleFor(e => e.UsuarioId)
                .NotNull().WithMessage(DespesaResources.UsuarioObrigatorio);
        }

        private void DescricaoObrigatorio()
        {
            RuleFor(e => e.Descricao)
                .NotNull().WithMessage(DespesaResources.DescricaoObrigatorio);
        }

        private void DescricaoTamanhoInvalido()
        {
            RuleFor(e => e.Descricao)
                .Length(5, 50).WithMessage(DespesaResources.DescricaoTamanhoInvalido)
                .When(e => !string.IsNullOrWhiteSpace(e.Descricao));
        }

        private void ValorObrigatorio()
        {
            RuleFor(e => e.Valor)
                .NotNull().WithMessage(DespesaResources.ValorObrigatorio);
        }

        private void VenctoObrigatorio()
        {
            RuleFor(e => e.Vencto)
                .NotNull().WithMessage(DespesaResources.VenctoObrigatorio);
        }


        #endregion

        #region Regras Complexas

        private bool VerificarDescricaoNaoDuplicado(DespesaDTO dto, string nome)
        {

            return despesaRepository.DescricaoNaoCadastrado(dto.Id, nome);

        }

        private void DescricaoNaoDuplicado()
        {
            RuleFor(e => e.Descricao)
                .Must(VerificarDescricaoNaoDuplicado).WithMessage(DespesaResources.DescricaoDuplicado)
                .When(e => !string.IsNullOrWhiteSpace(e.Descricao));
        }



        //private bool VerificarUsuarioCadastroValido(CategoriaDTO dto, int? usuarioId)
        //{
        //    return categoriaRepository.UsuarioCadastroValido(dto.Id, usuarioId);

        //}


        //private void UsuarioCadastroValido()
        //{
        //    RuleFor(e => e.UsuarioId)
        //        .Must(VerificarUsuarioCadastroValido).WithMessage(CategoriaResources.UsuarioCadastroInvalido);
        //}


        //private bool VerificarIdExiste(CategoriaDTO dto, int? usuarioId)
        //{
        //    return categoriaRepository.IdCategoriaExiste(dto.Id);

        //}


        //private void IdValidoExistente()
        //{
        //    RuleFor(e => e.UsuarioId)
        //        .Must(VerificarIdExiste).WithMessage(CategoriaResources.CategoriaNaoEncontrada);
        //}

        //private bool VerificarCategoriasVinculadas(int? id)
        //{
        //    return categoriaRepository.CategoriasPaiNaoVinculadas(id);

        //}
        //private void CategoriasVinculadas()
        //{
        //    RuleFor(e => e.Id)
        //        .Must(VerificarCategoriasVinculadas).WithMessage(CategoriaResources.CategoriaVinculadas);
        //}

        //private bool VerificarDespesasVinculadas(int? id)
        //{
        //    return despesaRepository.CategoriasNaoVinculadas(id);

        //}
        //private void DespesasVinculadas()
        //{
        //    RuleFor(e => e.Id)
        //        .Must(VerificarDespesasVinculadas).WithMessage(CategoriaResources.DespesasVinculadas);
        //}

        //private bool VerificarLancamentosVinculados(int? id)
        //{
        //    return lancamentoRepository.CategoriasNaoVinculadas(id);

        //}
        //private void LancamentosVinculados()
        //{
        //    RuleFor(e => e.Id)
        //        .Must(VerificarLancamentosVinculados).WithMessage(CategoriaResources.LancamentosVinculados);
        //}


        //private bool VerificarOrcamentosVinculadas(int? id)
        //{
        //    return orcamentoRepository.CategoriasNaoVinculadas(id);

        //}
        //private void OrcamentosVinculados()
        //{
        //    RuleFor(e => e.Id)
        //        .Must(VerificarOrcamentosVinculadas).WithMessage(CategoriaResources.OrcamentosVinculados);
        //}


        #endregion

        #region Operações de Validação de Domínio
        public void ValidarCadastro(DespesaDTO dto)
        {
            this.ValidateAndThrow(dto, "Cadastro");
        }

        public void ValidarEdicao(DespesaDTO dto)
        {
            this.ValidateAndThrow(dto, "Edicao");
        }

        public void ValidarExclusao(DespesaDTO dto)
        {
            this.ValidateAndThrow(dto, "Exclusao");
        }


        #endregion

     
    }
}


