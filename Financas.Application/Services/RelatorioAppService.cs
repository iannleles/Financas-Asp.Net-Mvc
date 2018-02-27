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

    public class RelatorioAppService : IRelatorioAppService
    {

        private readonly ILancamentoRepository lancamentoRepository;
        private readonly IOrcamentoRepository orcamentoRepository;
        private readonly ILancamentoAppService lancamentoAppService;

        public RelatorioAppService(
                                    ILancamentoRepository lancamentoRepository,
                                    IOrcamentoRepository orcamentoRepository,
                                    ILancamentoAppService lancamentoAppService)
        {

            this.lancamentoRepository = lancamentoRepository;
            this.orcamentoRepository = orcamentoRepository;
            this.lancamentoAppService = lancamentoAppService;
        }

        #region LancamentosPorPeriodo

        public List<RelatorioLancamentosPorPeriodoDTO> ObterRelatorioLancamentosPorPeriodo(out decimal saldoAnterior, int? usuarioId, DateTime dataInicial, DateTime dataFinal)
        {

            saldoAnterior = lancamentoRepository.ObterSaldoInicial(usuarioId, dataInicial.Day, dataInicial.Month, dataInicial.Year);

            return lancamentoRepository.ObterRelatorioLancamentosPorPeriodo(usuarioId, dataInicial, dataFinal);

        }

        #endregion

        #region LancamentosPorMeses

        public RelatorioLancamentosPorMesesDTO ObterRelatorioLancamentosPorMeses(int? usuarioId, int ano)
        {
            return lancamentoRepository.ObterRelatorioLancamentosPorMeses(usuarioId, ano);
        }

        #endregion

        #region ObterRelatorioPrevistoRealizado

        public List<RelatorioPrevistoRealizadoDTO> ObterRelatorioPrevistoRealizado(out decimal saldoAnterior, int? usuarioId, int ano, int mes)
        {
            saldoAnterior = lancamentoRepository.ObterSaldoInicial(usuarioId, 1, mes, ano);

            return lancamentoRepository.ObterRelatorioPrevistoRealizado(usuarioId, ano, mes);

        }

        #endregion

        public void Dispose()
        {

        }

    }
}
