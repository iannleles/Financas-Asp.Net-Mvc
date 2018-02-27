using System;
using Financas.Domain.Common.Specifications;
using Financas.Domain.Entities;

namespace Financas.Domain.Specs.DespesaSpecs
{
    public class DespesaPorPorIdDiferente : SpecificationBase<Despesa>
    {
        private readonly int? filtroId;

        public DespesaPorPorIdDiferente(int? id)
        {
            this.filtroId = id;
        }

        public override System.Linq.Expressions.Expression<Func<Despesa, bool>> SpecExpression
        {
            get { return item => item.Id != filtroId; }
        }
    }


}
