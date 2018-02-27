using Financas.Domain.Contracts.Repositories;
using Financas.Domain.Dtos;
using Financas.Domain.Resources;
using FluentValidation;



namespace Financas.Domain.Entidade.Validators
{
    public class CategoriaValidator : AbstractValidator<CategoriaDTO>
    {
        //Atributo
        private readonly ICategoriaRepository categoriaRepository;
        private readonly IDespesaRepository despesaRepository;
        private readonly ILancamentoRepository lancamentoRepository;
        private readonly IOrcamentoRepository orcamentoRepository;


        //Construtor
        public CategoriaValidator(
            ICategoriaRepository categoriaRepository,
            IDespesaRepository despesaRepository,
            ILancamentoRepository lancamentoRepository,
            IOrcamentoRepository orcamentoRepository)
        {

            this.categoriaRepository = categoriaRepository;
            this.despesaRepository = despesaRepository;
            this.lancamentoRepository = lancamentoRepository;
            this.orcamentoRepository = orcamentoRepository;


            RuleSet("Cadastro", () =>
            {
                UsuarioIdObrigatorio();
                NomeObrigatorio();
                TipoObrigatorio();
                NomeTamanhoInvalido();
                TipoTamanhoInvalido();
                NomeNaoDuplicado();
            });

            RuleSet("Edicao", () =>
            {
                IdObrigatorio();
                UsuarioIdObrigatorio();
                NomeObrigatorio();
                TipoObrigatorio();
                NomeTamanhoInvalido();
                TipoTamanhoInvalido();
                NomeNaoDuplicado();
                UsuarioCadastroValido();
            });

            RuleSet("Exclusao", () =>
            {
                IdValidoExistente();
                UsuarioCadastroValido();
                CategoriasVinculadas();
                DespesasVinculadas();
                LancamentosVinculados();
                OrcamentosVinculados();
            });

        }

        #region Regras Simples

        private void IdObrigatorio()
        {
            RuleFor(e => e.Id)
                .NotNull().WithMessage(CategoriaResources.IdObrigatorio);
        }

        private void UsuarioIdObrigatorio()
        {
            RuleFor(e => e.UsuarioId)
                .NotNull().WithMessage(CategoriaResources.UsuarioObrigatorio);
        }

        private void NomeObrigatorio()
        {
            RuleFor(e => e.Nome)
                .NotNull().WithMessage(CategoriaResources.NomeObrigatorio);
        }

        private void TipoObrigatorio()
        {
            RuleFor(e => e.Tipo)
                .NotNull().WithMessage(CategoriaResources.TipoObrigatorio);
        }

        private void NomeTamanhoInvalido()
        {
            RuleFor(e => e.Nome)
                .Length(5, 50).WithMessage(CategoriaResources.NomeTamanhoInvalido)
                .When(e => !string.IsNullOrWhiteSpace(e.Nome));
        }

        private void TipoTamanhoInvalido()
        {
            RuleFor(e => e.Tipo)
                .Length(1, 1).WithMessage(CategoriaResources.TipoTamanhoInvalido)
                .When(e => !string.IsNullOrWhiteSpace(e.Tipo));
        }

        #endregion

        #region Regras Complexas

        private bool VerificarNomeNaoDuplicado(CategoriaDTO dto, string nome)
        {
            return categoriaRepository.NomeNaoCadastrado(dto.Id, nome, dto.UsuarioId, dto.Tipo);

        }

        private void NomeNaoDuplicado()
        {
            RuleFor(e => e.Nome)
                .Must(VerificarNomeNaoDuplicado).WithMessage(CategoriaResources.NomeDuplicado)
                .When(e => !string.IsNullOrWhiteSpace(e.Nome));
        }



        private bool VerificarUsuarioCadastroValido(CategoriaDTO dto, int? usuarioId)
        {
            return categoriaRepository.UsuarioCadastroValido(dto.Id, usuarioId);

        }


        private void UsuarioCadastroValido()
        {
            RuleFor(e => e.UsuarioId)
                .Must(VerificarUsuarioCadastroValido).WithMessage(CategoriaResources.UsuarioCadastroInvalido);
        }


        private bool VerificarIdExiste(CategoriaDTO dto, int? usuarioId)
        {
            return categoriaRepository.IdCategoriaExiste(dto.Id);

        }


        private void IdValidoExistente()
        {
            RuleFor(e => e.UsuarioId)
                .Must(VerificarIdExiste).WithMessage(CategoriaResources.CategoriaNaoEncontrada);
        }

        private bool VerificarCategoriasVinculadas(int? id)
        {
            return categoriaRepository.CategoriasPaiNaoVinculadas(id);

        }
        private void CategoriasVinculadas()
        {
            RuleFor(e => e.Id)
                .Must(VerificarCategoriasVinculadas).WithMessage(CategoriaResources.CategoriaVinculadas);
        }

        private bool VerificarDespesasVinculadas(int? id)
        {
            return despesaRepository.CategoriasNaoVinculadas(id);

        }
        private void DespesasVinculadas()
        {
            RuleFor(e => e.Id)
                .Must(VerificarDespesasVinculadas).WithMessage(CategoriaResources.DespesasVinculadas);
        }

        private bool VerificarLancamentosVinculados(int? id)
        {
            return lancamentoRepository.CategoriasNaoVinculadas(id);

        }
        private void LancamentosVinculados()
        {
            RuleFor(e => e.Id)
                .Must(VerificarLancamentosVinculados).WithMessage(CategoriaResources.LancamentosVinculados);
        }


        private bool VerificarOrcamentosVinculadas(int? id)
        {
            return orcamentoRepository.CategoriasNaoVinculadas(id);

        }
        private void OrcamentosVinculados()
        {
            RuleFor(e => e.Id)
                .Must(VerificarOrcamentosVinculadas).WithMessage(CategoriaResources.OrcamentosVinculados);
        }


        #endregion

        #region Operações de Validação de Domínio
        public void ValidarCadastro(CategoriaDTO dto)
        {
            this.ValidateAndThrow(dto, "Cadastro");
        }

        public void ValidarEdicao(CategoriaDTO dto)
        {
            this.ValidateAndThrow(dto, "Edicao");
        }

        public void ValidarExclusao(CategoriaDTO dto)
        {
            this.ValidateAndThrow(dto, "Exclusao");
        }


        #endregion


    }
}


