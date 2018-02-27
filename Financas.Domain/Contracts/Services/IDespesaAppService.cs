using System;
using System.Linq;
using Financas.Domain.Dtos;
using Financas.Domain.Entities;
using Financas.Domain.ValueObjects;

namespace Financas.Domain.Contracts.Services
{
    public interface IDespesaAppService : IDisposable
    {
        void CadastrarDespesa(DespesaDTO categoriaDTO);

        void EditarDespesa(DespesaDTO categoriaDTO);

        void ExcluirDespesa(int despesaId);

        Despesa GetById(int Id);

        IQueryable<Despesa> ListarDespesaFixas(int? usuarioId);


    }
}
