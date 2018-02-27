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
using System.Globalization;

namespace Financas.Application.Services
{

    public class LancamentoAppService : ILancamentoAppService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILancamentoRepository lancamentoRepository;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly LancamentoValidator lancamentoValidator;

        public LancamentoAppService(
            IUnitOfWork unitOfWork,
            ILancamentoRepository lancamentoRepository,
            IUsuarioRepository usuarioRepository)
        {
            this.unitOfWork = unitOfWork;
            this.lancamentoRepository = lancamentoRepository;
            this.usuarioRepository = usuarioRepository;
            this.lancamentoValidator = new LancamentoValidator();
        }


        public void CadastrarLancamento(LancamentoDTO dto)
        {

            Lancamento lancamento = Lancamento.Cadastrar(dto, lancamentoValidator);

            unitOfWork.BeginTransaction();

            lancamentoRepository.Add(lancamento);

            unitOfWork.Commit();


        }


        public void EditarLancamento(LancamentoDTO dto)
        {

            Lancamento lancamento = lancamentoRepository.GetById(dto.Id);

            lancamento.Editar(dto, lancamentoValidator);

            unitOfWork.BeginTransaction();

            lancamentoRepository.Update(lancamento);

            unitOfWork.Commit();


        }

        public void ExcluirLancamento(int id)
        {

            Lancamento lancamento = lancamentoRepository.GetById(id);

            lancamento.Excluir(lancamentoValidator);

            unitOfWork.BeginTransaction();

            lancamentoRepository.Delete(id);

            unitOfWork.Commit();


        }

        public Lancamento GetById(int Id)
        {
            return lancamentoRepository.GetById(Id);
        }

        public List<LancamentoMensalDTO> ObterLancamentosMensal(out decimal saldoAnterior, int? usuarioId, int mes, int ano)
        {

            saldoAnterior = lancamentoRepository.ObterSaldoInicial(usuarioId, 1, mes, ano);

            return lancamentoRepository.ObterLancamentosMensal(usuarioId, mes, ano);

        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }



}
