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

    public class DashboardAppService : IDashboardAppService
    {
        private readonly ILancamentoRepository lancamentoRepository;
        private readonly ILancamentoAppService lancamentoAppService;
        private readonly IUsuarioRepository usuarioRepository;

        public DashboardAppService(
            ILancamentoRepository lancamentoRepository,
            IUsuarioRepository usuarioRepository,
            ILancamentoAppService lancamentoAppService)
        {
            this.lancamentoRepository = lancamentoRepository;
            this.usuarioRepository = usuarioRepository;
            this.lancamentoAppService = lancamentoAppService;
        }


        public DashboardHomeDTO ObterDashboardHome(int? usuarioId, int ano, int mes)
        {
            DashboardHomeDTO dto = lancamentoRepository.ObterDashboardHome(usuarioId, ano, mes);

            return dto;

        }

        public DashboardPrevistoRealizadoDTO ObterDashboardPrevistoRealizado(int? usuarioId, int ano, int mes)
        {
            DashboardPrevistoRealizadoDTO dto = lancamentoRepository.ObterDashboardPrevistoRealizado(usuarioId, ano, mes);

            return dto;

        }

        public DashboardDiarioDTO ObterDashboardDiario(int? usuarioId, int ano, int mes)
        {
            DashboardDiarioDTO dto = lancamentoRepository.ObterDashboardDiario(usuarioId, ano, mes);

            return dto;

        }


        public List<DashboardAnosDTO> ObterDashboardAnos(int? usuarioId, int anoInicial, int anoFinal)
        {
            return lancamentoRepository.ObterDashboardAnos(usuarioId, anoInicial, anoFinal);

        }

        public List<DashboardMesesDTO> ObterDashboardMeses(int? usuarioId, int ano)
        {
            return lancamentoRepository.ObterDashboardMeses(usuarioId, ano);

        }


        public void Dispose()
        {
            
        }
    }
}
