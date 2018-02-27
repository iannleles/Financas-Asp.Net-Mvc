
using System;
using Financas.Domain.Common.Specifications;
using Financas.Domain.Entities;
using System.Globalization;

namespace Financas.Domain.Specs.LancamentoSpecs
{
    public class LancamentosPorUsuarioAnoInicialFinal : SpecificationBase<Lancamento>
    {
        private readonly int? filtroUsuarioId;
        private readonly int filtroAnoInicial;
        private readonly int filtroAnoFinal;

        public LancamentosPorUsuarioAnoInicialFinal(int? usuarioId, int anoInicial, int anoFinal)
        {
            this.filtroUsuarioId = usuarioId;
            this.filtroAnoInicial = anoInicial;
            this.filtroAnoFinal = anoFinal;
        }

        public override System.Linq.Expressions.Expression<Func<Lancamento, bool>> SpecExpression
        {
            get { return item => item.UsuarioId == filtroUsuarioId && item.Data.Year >= filtroAnoInicial && item.Data.Year <= filtroAnoFinal; }
        }
    }


}



