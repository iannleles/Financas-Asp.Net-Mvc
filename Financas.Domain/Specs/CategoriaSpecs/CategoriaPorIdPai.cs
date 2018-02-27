
using System;
using Financas.Domain.Common.Specifications;
using Financas.Domain.Entities;

namespace Financas.Domain.Specs.CategoriaSpecs
{
    public class CategoriaPorIdPai : SpecificationBase<Categoria>
    {
        private readonly int? filtroIdPai;

        public CategoriaPorIdPai(int? id)
        {
            this.filtroIdPai = id;
        }

        public override System.Linq.Expressions.Expression<Func<Categoria, bool>> SpecExpression
        {
            get { return item => item.CategoriaPaiId == filtroIdPai; }
        }
    }


}
