using System;
using Financas.Domain.Common.Specifications;
using Financas.Domain.Entities;

namespace Financas.Domain.Specs.CategoriaSpecs
{
    public class CategoriaPorPorIdDiferente : SpecificationBase<Categoria>
    {
        private readonly int? filtroId;

        public CategoriaPorPorIdDiferente(int? id)
        {
            this.filtroId = id;
        }

        public override System.Linq.Expressions.Expression<Func<Categoria, bool>> SpecExpression
        {
            get { return item => item.Id != filtroId; }
        }
    }


}
