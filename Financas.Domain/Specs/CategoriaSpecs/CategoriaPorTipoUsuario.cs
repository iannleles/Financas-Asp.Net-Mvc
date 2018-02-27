using System;
using Financas.Domain.Common.Specifications;
using Financas.Domain.Entities;

namespace Financas.Domain.Specs.CategoriaSpecs
{


    public class CategoriaPorTipoUsuario : SpecificationBase<Categoria>
    {
        private readonly string filtroTipo;
        private readonly int filtroUsuarioId;

        public CategoriaPorTipoUsuario(string tipo, int usuarioId)
        {
            this.filtroTipo = tipo;
            this.filtroUsuarioId = usuarioId;
        }

        public override System.Linq.Expressions.Expression<Func<Categoria, bool>> SpecExpression
        {
            get { return item => item.Tipo.ToUpper() == filtroTipo.ToUpper() && item.UsuarioId == filtroUsuarioId; }
        }
    }


}
