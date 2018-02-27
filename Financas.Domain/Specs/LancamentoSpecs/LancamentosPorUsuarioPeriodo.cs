
using System;
using Financas.Domain.Common.Specifications;
using Financas.Domain.Entities;
using System.Globalization;

namespace Financas.Domain.Specs.LancamentoSpecs
{
    public class LancamentosPorUsuarioPeriodo : SpecificationBase<Lancamento>
    {
        private readonly int? filtroUsuarioId;
        private readonly DateTime filtroDataInicial;
        private readonly DateTime filtroDataFinal;

        public LancamentosPorUsuarioPeriodo(int? usuarioId, DateTime dataInicial, DateTime dataFinal)
        {
            this.filtroUsuarioId = usuarioId;
            this.filtroDataInicial = dataInicial;
            this.filtroDataFinal = dataFinal;
        }

        public override System.Linq.Expressions.Expression<Func<Lancamento, bool>> SpecExpression
        {
            get { return item => item.UsuarioId == filtroUsuarioId && item.Data >= filtroDataInicial && item.Data <= filtroDataFinal; }
        }
    }


}
