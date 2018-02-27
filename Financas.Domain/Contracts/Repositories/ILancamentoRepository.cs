using Financas.Domain.Contracts.Repositories.Common;
using Financas.Domain.Dtos;
using Financas.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Financas.Domain.Contracts.Repositories
{
    public interface ILancamentoRepository : IGenericRepository<Lancamento>
    {
  
        #region Dashboards

        DashboardHomeDTO ObterDashboardHome(int? usuarioId, int ano, int mes);

        DashboardPrevistoRealizadoDTO ObterDashboardPrevistoRealizado(int? usuarioId, int ano, int mes);

        DashboardDiarioDTO ObterDashboardDiario(int? usuarioId, int ano, int mes);

        List<DashboardMesesDTO> ObterDashboardMeses(int? usuarioId, int ano);

        List<DashboardAnosDTO> ObterDashboardAnos(int? usuarioId, int anoInicial, int anoFinal);

        #endregion

        #region Relatorios

        List<RelatorioPrevistoRealizadoDTO> ObterRelatorioPrevistoRealizado(int? usuarioId, int ano, int mes);

        RelatorioLancamentosPorMesesDTO ObterRelatorioLancamentosPorMeses(int? usuarioId, int ano);

        List<RelatorioLancamentosPorPeriodoDTO> ObterRelatorioLancamentosPorPeriodo(int? usuarioId, DateTime dataInicial, DateTime dataFinal);

        #endregion

        #region Outros

        List<LancamentoMensalDTO> ObterLancamentosMensal(int? usuarioId, int mes, int ano);
        decimal ObterSaldoInicial(int? usuarioId, int dia, int mes, int ano);
        bool CategoriasNaoVinculadas(int? categoriaId);
         
        #endregion

    }
}
