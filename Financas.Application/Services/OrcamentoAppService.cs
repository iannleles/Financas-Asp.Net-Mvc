
using System.Linq;
using Financas.Domain.Common.Specifications;
using Financas.Domain.Contracts.Repositories;
using Financas.Domain.Contracts.Repositories.Common;
using Financas.Domain.Contracts.Services;
using Financas.Domain.Dtos;
using Financas.Domain.Entidade.Validators;
using Financas.Domain.Entities;
using Financas.Domain.Specs.LancamentoSpecs;
using System.Collections.Generic;
using System;

namespace Financas.Application.Services
{

    public class OrcamentoAppService : IOrcamentoAppService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IOrcamentoRepository orcamentoRepository;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly OrcamentoValidator orcamentoValidator;

        public OrcamentoAppService(
            IUnitOfWork unitOfWork,
            IOrcamentoRepository orcamentoRepository,
            IUsuarioRepository usuarioRepository)
        {
            this.unitOfWork = unitOfWork;
            this.orcamentoRepository = orcamentoRepository;
            this.usuarioRepository = usuarioRepository;
            this.orcamentoValidator = new OrcamentoValidator();
        }


        public void CadastrarOrcamento(OrcamentoDTO dto)
        {

            Orcamento orcamento = Orcamento.Cadastrar(dto, orcamentoValidator);

            unitOfWork.BeginTransaction();

            orcamentoRepository.Add(orcamento);

            unitOfWork.Commit();


        }


        public void EditarOrcamento(OrcamentoDTO dto)
        {

            Orcamento orcamento = orcamentoRepository.GetById(dto.Id);

            orcamento.Editar(dto, orcamentoValidator);

            unitOfWork.BeginTransaction();

            orcamentoRepository.Update(orcamento);

            unitOfWork.Commit();


        }

        public void ExcluirOrcamento(int id)
        {

            Orcamento orcamento = orcamentoRepository.GetById(id);

            orcamento.Excluir(orcamentoValidator);

            unitOfWork.BeginTransaction();

            orcamentoRepository.Delete(id);

            unitOfWork.Commit();


        }

        public Orcamento GetById(int Id)
        {
            return orcamentoRepository.GetById(Id);
        }

        public IQueryable<Orcamento> OrcamentosPorUsuarioAnoMes(int? usuarioId, int ano, int mes)
        {

            ISpecification<Orcamento> criterio = new OrcamentosPorUsuarioAnoMes(usuarioId.Value, ano, mes);

            return orcamentoRepository.Get(criterio).AsQueryable();

        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }



}
