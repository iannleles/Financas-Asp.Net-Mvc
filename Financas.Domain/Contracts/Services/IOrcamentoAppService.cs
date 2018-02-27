using System;
using System.Linq;
using Financas.Domain.Dtos;
using Financas.Domain.Entities;
using Financas.Domain.ValueObjects;
using System.Collections.Generic;

namespace Financas.Domain.Contracts.Services
{
    public interface IOrcamentoAppService : IDisposable
    {
        void CadastrarOrcamento(OrcamentoDTO dto);

        void EditarOrcamento(OrcamentoDTO dto);

        void ExcluirOrcamento(int id);

        Orcamento GetById(int Id);

        IQueryable<Orcamento> OrcamentosPorUsuarioAnoMes(int? usuarioId, int ano, int mes);

    }
}
