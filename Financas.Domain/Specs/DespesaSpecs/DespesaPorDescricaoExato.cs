using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financas.Domain.Common.Specifications;
using Financas.Domain.Entities;

namespace Financas.Domain.Specs.DespesaSpecs
{
    public class DespesaPorDescricaoExato : SpecificationBase<Despesa>
    {
        private readonly string filtroDescricaoExato;

        public DespesaPorDescricaoExato(string descricaoExato)
        {
            this.filtroDescricaoExato = descricaoExato;
        }

        public override System.Linq.Expressions.Expression<Func<Despesa, bool>> SpecExpression
        {
            get { return item => item.Descricao.ToUpper() == this.filtroDescricaoExato.ToUpper(); }
        }
    }
}
