using System;
using Financas.Domain.Common.Specifications;
using Financas.Domain.Entities;

namespace Financas.Domain.Specs.CategoriaSpecs
{
    public class DespesaPorUsuario : SpecificationBase<Despesa>
    {
        private readonly int filtroUsuarioId;

        public DespesaPorUsuario(int usuarioId)
        {
            this.filtroUsuarioId = usuarioId;
        }

        public override System.Linq.Expressions.Expression<Func<Despesa, bool>> SpecExpression
        {
            get { return item => item.UsuarioId == filtroUsuarioId; }
        }
    }
}
