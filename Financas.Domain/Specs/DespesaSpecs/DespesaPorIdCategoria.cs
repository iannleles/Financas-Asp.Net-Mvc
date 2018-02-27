
using System;
using Financas.Domain.Common.Specifications;
using Financas.Domain.Entities;

namespace Financas.Domain.Specs.DespesaSpecs
{
    public class DespesaPorIdCategoria : SpecificationBase<Despesa>
    {
        private readonly int? filtroCategoriaId;

        public DespesaPorIdCategoria(int? categoriaId)
        {
            this.filtroCategoriaId = categoriaId;
        }

        public override System.Linq.Expressions.Expression<Func<Despesa, bool>> SpecExpression
        {
            get { return item => item.CategoriaId == filtroCategoriaId; }
        }
    }


}
