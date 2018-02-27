
using System;
using Financas.Domain.Common.Specifications;
using Financas.Domain.Entities;

namespace Financas.Domain.Specs.LancamentoSpecs
{
    public class LancamentosPorUsuarioAnoMes : SpecificationBase<Lancamento>
    {
        private readonly int? filtroUsuarioId;
        private readonly int? filtroAno;
        private readonly int? filtroMes;

        public LancamentosPorUsuarioAnoMes(int? usuarioId, int ano, int mes)
        {
            this.filtroUsuarioId = usuarioId;
            this.filtroAno = ano;
            this.filtroMes = mes;
        }

        public override System.Linq.Expressions.Expression<Func<Lancamento, bool>> SpecExpression
        {
            get { return item => item.UsuarioId == filtroUsuarioId && item.Data.Year == filtroAno && item.Data.Month == filtroMes ; }
        }
    }


}
