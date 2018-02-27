
using System;
using Financas.Domain.Common.Specifications;
using Financas.Domain.Entities;
using System.Globalization;

namespace Financas.Domain.Specs.LancamentoSpecs
{
    public class LancamentosPorUsuarioAnual : SpecificationBase<Lancamento>
    {
        private readonly int? filtroUsuarioId;
        private readonly int filtroAno;

        public LancamentosPorUsuarioAnual(int? usuarioId, int ano)
        {
            this.filtroUsuarioId = usuarioId;
            this.filtroAno = ano;
        }

        public override System.Linq.Expressions.Expression<Func<Lancamento, bool>> SpecExpression
        {
            get { return item => item.UsuarioId == filtroUsuarioId && item.Data.Year == filtroAno; }
        }
    }


}



