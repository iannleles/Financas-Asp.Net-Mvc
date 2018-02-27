
using System;
using Financas.Domain.Common.Specifications;
using Financas.Domain.Entities;

namespace Financas.Domain.Specs.LancamentoSpecs
{
    public class OrcamentosPorUsuarioAnoMes : SpecificationBase<Orcamento>
    {
        private readonly int? filtroUsuarioId;
        private readonly int? filtroAno;
        private readonly int? filtroMes;

        public OrcamentosPorUsuarioAnoMes(int? usuarioId, int ano, int mes)
        {
            this.filtroUsuarioId = usuarioId;
            this.filtroAno = ano;
            this.filtroMes = mes;
        }

        public override System.Linq.Expressions.Expression<Func<Orcamento, bool>> SpecExpression
        {
            get { return item => item.UsuarioId == filtroUsuarioId && item.Ano == filtroAno && item.Mes == filtroMes ; }
        }
    }


}
