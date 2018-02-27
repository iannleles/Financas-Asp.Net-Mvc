using System;
using Financas.Domain.Common.Specifications;
using Financas.Domain.Entities;

namespace Financas.Domain.Specs.CategoriaSpecs
{
    public class CategoriaPorUsuario : SpecificationBase<Categoria>
    {
        private readonly int filtroUsuarioId;

        public CategoriaPorUsuario(int usuarioId)
        {
            this.filtroUsuarioId = usuarioId;
        }

        public override System.Linq.Expressions.Expression<Func<Categoria, bool>> SpecExpression
        {
            get { return item => item.UsuarioId == filtroUsuarioId; }
        }
    }



}
