
using System;
using System.Linq;
using Financas.Domain.Dtos;
using Financas.Domain.Entities;
using Financas.Domain.ValueObjects;
using System.Collections.Generic;

namespace Financas.Domain.Contracts.Services
{
    public interface ILancamentoAppService : IDisposable
    {
        void CadastrarLancamento(LancamentoDTO dto);

        void EditarLancamento(LancamentoDTO dto);

        void ExcluirLancamento(int id);

        Lancamento GetById(int Id);

        List<LancamentoMensalDTO> ObterLancamentosMensal(out decimal saldoAnterior, int? usuarioId, int mes, int ano);

    }
}
