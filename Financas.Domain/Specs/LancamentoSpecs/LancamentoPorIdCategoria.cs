
using System;
using Financas.Domain.Common.Specifications;
using Financas.Domain.Entities;

namespace Financas.Domain.Specs.LancamentoSpecs
{
    public class LancamentoPorIdCategoria : SpecificationBase<Lancamento>
    {
        private readonly int? filtroCategoriaId;

        public LancamentoPorIdCategoria(int? categoriaId)
        {
            this.filtroCategoriaId = categoriaId;
        }

        public override System.Linq.Expressions.Expression<Func<Lancamento, bool>> SpecExpression
        {
            get { return item => item.CategoriaId == filtroCategoriaId; }
        }
    }


}
