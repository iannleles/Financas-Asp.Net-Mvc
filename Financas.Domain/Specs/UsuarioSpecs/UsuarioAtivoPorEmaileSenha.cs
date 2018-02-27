using System;
using Financas.Domain.Common.Specifications;
using Financas.Domain.Entities;

namespace Financas.Domain.Specs.UsuarioSpecs
{
    public class UsuarioAtivoPorEmaileSenha : SpecificationBase<Usuario>
    {
       private readonly string filtroEmail;
       private readonly string filtroSenha;

       public UsuarioAtivoPorEmaileSenha(string email, string senha)
       {
            this.filtroEmail = email;
            this.filtroSenha = senha;
       }

       public override System.Linq.Expressions.Expression<Func<Usuario, bool>> SpecExpression
        {
            get { return item => item.Ativo == true && item.Email.ToUpper() == filtroEmail.ToUpper() && item.Senha.ToUpper() == filtroSenha.ToUpper(); }
        }
    }
}
