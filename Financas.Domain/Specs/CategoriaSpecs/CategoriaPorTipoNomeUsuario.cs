using System;
using Financas.Domain.Common.Specifications;
using Financas.Domain.Entities;

namespace Financas.Domain.Specs.CategoriaSpecs
{
    public class CategoriaPorTipoNomeUsuario : SpecificationBase<Categoria>
    {
        private readonly string filtroTipo;
        private readonly string filtroNome;
        private readonly int filtroUsuarioId;

        public CategoriaPorTipoNomeUsuario(string tipo, string nome, int usuarioId)
        {
            this.filtroTipo = tipo;
            this.filtroNome = nome;
            this.filtroUsuarioId = usuarioId;
        }

        public override System.Linq.Expressions.Expression<Func<Categoria, bool>> SpecExpression
        {
            get { return item => item.Tipo.ToUpper() == filtroTipo.ToUpper() && item.Nome.Contains(filtroNome) && item.UsuarioId == filtroUsuarioId; }
        }
    }
}
