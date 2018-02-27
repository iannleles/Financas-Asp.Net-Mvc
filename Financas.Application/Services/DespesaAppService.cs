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

    public class DespesaAppService : IDespesaAppService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDespesaRepository despesaRepository;
        private readonly DespesaValidator despesaValidator;

        public DespesaAppService(
            IUnitOfWork unitOfWork,
            IDespesaRepository despesaRepository
        )
        {
            this.unitOfWork = unitOfWork;

            this.despesaRepository = despesaRepository;

            this.despesaValidator = new DespesaValidator(despesaRepository);
        }


        public void CadastrarDespesa(DespesaDTO dto)
        {

            Despesa despesa = Despesa.Cadastrar(dto, despesaValidator);

            unitOfWork.BeginTransaction();

            despesaRepository.Add(despesa);

            unitOfWork.Commit();


        }

        public void EditarDespesa(DespesaDTO dto)
        {

            Despesa despesa = despesaRepository.GetById(dto.Id);

            despesa.Editar(dto, despesaValidator);

            unitOfWork.BeginTransaction();

            despesaRepository.Update(despesa);

            unitOfWork.Commit();

        }

        public void ExcluirDespesa(int id)
        {

            Despesa despesa = despesaRepository.GetById(id);

            despesa.Excluir(despesaValidator);

            unitOfWork.BeginTransaction();

            despesaRepository.Delete(id);

            unitOfWork.Commit();

        }

        public Despesa GetById(int Id)
        {
            return despesaRepository.GetById(Id);
        }


        public IQueryable<Despesa> ListarDespesaFixas(int? usuarioId)
        {
            ISpecification<Despesa> criterio = new DespesaPorUsuario(usuarioId.Value);
            return despesaRepository.Get(criterio).AsQueryable();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }



}
