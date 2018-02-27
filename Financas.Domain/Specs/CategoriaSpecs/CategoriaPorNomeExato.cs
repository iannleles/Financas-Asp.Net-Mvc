using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financas.Domain.Common.Specifications;
using Financas.Domain.Entities;

namespace Financas.Domain.Specs.CategoriaSpecs
{
    public class CategoriaPorNomeExato : SpecificationBase<Categoria>
    {
        private readonly string filtroNomeExato;

        public CategoriaPorNomeExato(string nomeExato)
        {
            this.filtroNomeExato = nomeExato;
        }

        public override System.Linq.Expressions.Expression<Func<Categoria, bool>> SpecExpression
        {
            get { return item => item.Nome.ToUpper() == this.filtroNomeExato.ToUpper(); }
        }
    }
}
