
using Financas.Domain.Common.Specifications;
using Financas.Domain.Entities;
using System;
public class CategoriaPorUsuarioValido : SpecificationBase<Categoria>
{
    private readonly int? filtroId;
    private readonly int? filtroUsuarioId;

    public CategoriaPorUsuarioValido(int? id, int? usuarioId)
    {
        this.filtroId = id;
        this.filtroUsuarioId = usuarioId;
    }

    public override System.Linq.Expressions.Expression<Func<Categoria, bool>> SpecExpression
    {
        get { return item => item.Id == filtroId && item.UsuarioId == filtroUsuarioId; }
    }
}
