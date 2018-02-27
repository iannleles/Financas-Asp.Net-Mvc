
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financas.Domain.Common.Specifications;
using Financas.Domain.Entities;

namespace Financas.Domain.Specs.CategoriaSpecs
{
    public class CategoriaPorTipo : SpecificationBase<Categoria>
    {
        private readonly string filtroTipo;

        public CategoriaPorTipo(string tipo)
        {
            this.filtroTipo = tipo;
        }

        public override System.Linq.Expressions.Expression<Func<Categoria, bool>> SpecExpression
        {
            get { return item => item.Tipo.ToUpper() == this.filtroTipo.ToUpper(); }
        }
    }
}
