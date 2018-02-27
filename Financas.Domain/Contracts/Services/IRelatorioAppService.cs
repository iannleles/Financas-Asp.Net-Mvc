
using System;
using System.Linq;
using Financas.Domain.Dtos;
using Financas.Domain.Entities;
using Financas.Domain.ValueObjects;
using System.Collections.Generic;

namespace Financas.Domain.Contracts.Services
{
    public interface IRelatorioAppService : IDisposable
    {

        List<RelatorioLancamentosPorPeriodoDTO> ObterRelatorioLancamentosPorPeriodo(out decimal saldoAnterior, int? usuarioId, DateTime dataInicial, DateTime dataFinal);

        RelatorioLancamentosPorMesesDTO ObterRelatorioLancamentosPorMeses(int? usuarioId, int ano);

        List<RelatorioPrevistoRealizadoDTO> ObterRelatorioPrevistoRealizado(out decimal saldoAnterior, int? usuarioId, int ano, int mes);

    }
}
