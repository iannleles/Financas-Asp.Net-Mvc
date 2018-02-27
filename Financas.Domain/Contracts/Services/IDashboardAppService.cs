
using System;
using System.Linq;
using Financas.Domain.Dtos;
using Financas.Domain.Entities;
using Financas.Domain.ValueObjects;
using System.Collections.Generic;

namespace Financas.Domain.Contracts.Services
{
    public interface IDashboardAppService : IDisposable
    {

        DashboardHomeDTO ObterDashboardHome(int? usuarioId, int ano, int mes);

        DashboardPrevistoRealizadoDTO ObterDashboardPrevistoRealizado(int? usuarioId, int ano, int mes);

        DashboardDiarioDTO ObterDashboardDiario(int? usuarioId, int ano, int mes);

        List<DashboardAnosDTO> ObterDashboardAnos(int? usuarioId, int anoInicial, int anoFinal);

        List<DashboardMesesDTO> ObterDashboardMeses(int? usuarioId, int ano);

        
    }
}


