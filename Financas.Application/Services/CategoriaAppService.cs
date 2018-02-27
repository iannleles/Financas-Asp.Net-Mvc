using System.Linq;
using Financas.Domain.Common.Specifications;
using Financas.Domain.Contracts.Repositories;
using Financas.Domain.Contracts.Repositories.Common;
using Financas.Domain.Contracts.Services;
using Financas.Domain.Dtos;
using Financas.Domain.Entidade.Validators;
using Financas.Domain.Entities;
using Financas.Domain.Specs.CategoriaSpecs;
using Financas.Domain.ValueObjects;
using FluentValidation;

namespace Financas.Application.Services
{

    public class CategoriaAppService : ICategoriaAppService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICategoriaRepository categoriaRepository;
        private readonly IDespesaRepository despesaRepository;
        private readonly ILancamentoRepository lancamentoRepository;
        private readonly IOrcamentoRepository orcamentoRepository;
        private readonly CategoriaValidator categoriaValidator;
        public CategoriaAppService(
            IUnitOfWork unitOfWork,
            ICategoriaRepository categoriaRepository,
            IDespesaRepository despesaRepository,
            ILancamentoRepository lancamentoRepository,
            IOrcamentoRepository orcamentoRepository)
        {
            this.unitOfWork = unitOfWork;
            this.categoriaRepository = categoriaRepository;
            this.despesaRepository = despesaRepository;
            this.lancamentoRepository = lancamentoRepository;
            this.orcamentoRepository = orcamentoRepository;

            this.categoriaValidator = new CategoriaValidator(
                                            categoriaRepository,
                                            despesaRepository,
                                            lancamentoRepository,
                                            orcamentoRepository);
        }


        public void CadastrarCategoria(CategoriaDTO dto)
        {

            Categoria categoria = Categoria.Cadastrar(dto, categoriaValidator);

            unitOfWork.BeginTransaction();

            categoriaRepository.Add(categoria);

            unitOfWork.Commit();


        }

        public void EditarCategoria(CategoriaDTO dto)
        {

            Categoria categoria = categoriaRepository.GetById(dto.Id);

            categoria.Editar(dto, categoriaValidator);

            unitOfWork.BeginTransaction();

            categoriaRepository.Update(categoria);

            unitOfWork.Commit();


        }

        public void ExcluirCategoria(int id)
        {

            Categoria categoria = categoriaRepository.GetById(id);

            categoria.Excluir(categoriaValidator);

            unitOfWork.BeginTransaction();

            categoriaRepository.Delete(id);

            unitOfWork.Commit();


        }

        public Categoria GetById(int Id)
        {
            return categoriaRepository.GetById(Id);
        }

        public IQueryable<Categoria> ListarCategoriasPorTipo(string tipo, int? usuarioId)
        {
            ISpecification<Categoria> criterio = new CategoriaPorTipoUsuario(tipo, usuarioId.Value);
            return categoriaRepository.Get(criterio).AsQueryable();
        }

        public IQueryable<Categoria> ListarCategoriasPorUsuario(int? usuarioId)
        {
            ISpecification<Categoria> criterio = new CategoriaPorUsuario(usuarioId.Value);
            return categoriaRepository.Get(criterio).AsQueryable();
        }

        public IQueryable<Categoria> ListarCategoriaPaginacao(out int recordCount, string tipo, string nome, int? usuarioId, SortField[] sorts, int pageSize, int page)
        {
            ISpecification<Categoria> criterio = new CategoriaPorTipoNomeUsuario(tipo, nome, usuarioId.Value);
            return categoriaRepository.Get(out recordCount, sorts, criterio, pageSize, page).AsQueryable();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }



}
