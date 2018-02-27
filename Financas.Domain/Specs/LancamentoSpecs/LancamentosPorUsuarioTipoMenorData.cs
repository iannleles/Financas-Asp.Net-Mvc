
using System;
using Financas.Domain.Common.Specifications;
using Financas.Domain.Entities;
using System.Globalization;

namespace Financas.Domain.Specs.LancamentoSpecs
{
    public class LancamentosPorUsuarioTipoMenorData : SpecificationBase<Lancamento>
    {
        private readonly int? filtroUsuarioId;
        private readonly string filtroTipo;
        private readonly DateTime filtroData;

        public LancamentosPorUsuarioTipoMenorData(int? usuarioId, string tipo, DateTime datainicial)
        {
            this.filtroUsuarioId = usuarioId;
            this.filtroTipo = tipo;
            this.filtroData = datainicial;
        }

        public override System.Linq.Expressions.Expression<Func<Lancamento, bool>> SpecExpression
        {
            get { return item => item.UsuarioId == filtroUsuarioId && item.Tipo == filtroTipo & item.Data < filtroData; }
        }
    }


}
