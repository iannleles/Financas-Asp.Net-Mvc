
using System;
using Financas.Domain.Common.Specifications;
using Financas.Domain.Entities;

namespace Financas.Domain.Specs.OrcamentoSpecs
{
    public class OrcamentoPorIdCategoria : SpecificationBase<Orcamento>
    {
        private readonly int? filtroCategoriaId;

        public OrcamentoPorIdCategoria(int? categoriaId)
        {
            this.filtroCategoriaId = categoriaId;
        }

        public override System.Linq.Expressions.Expression<Func<Orcamento, bool>> SpecExpression
        {
            get { return item => item.CategoriaId == filtroCategoriaId; }
        }
    }


}
