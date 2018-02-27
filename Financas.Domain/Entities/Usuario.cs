using System;
using System.Collections.Generic;
using Financas.Domain.Dtos;
using Financas.Domain.Entidade.Validators;

namespace Financas.Domain.Entities
{
    public class Usuario
    {

        #region Propriedades

        public int? Id { get; private set; }

        public String Nome { get; private set; }

        public String Email { get; private set; }

        public String Senha { get; private set; }

        public Decimal SaldoInicial { get; private set; }

        public DateTime DataCadastro { get; private set; }

        public Boolean Ativo { get; private set; }

        public virtual ICollection<Categoria> Categorias { get; private set; }

        public virtual ICollection<Despesa> Despesas { get; private set; }

        public virtual ICollection<Lancamento> Lancamentos { get; private set; }

        public virtual ICollection<Orcamento> Orcamentos { get; private set; }


        #endregion

        #region Construtores

        protected Usuario()
		{
            Categorias = new HashSet<Categoria>();
            Despesas = new HashSet<Despesa>();
            Lancamentos = new HashSet<Lancamento>();
            Orcamentos = new HashSet<Orcamento>();
		}

        private Usuario(UsuarioDTO dto)
        {
            this.Nome = dto.Nome;
            this.Email = dto.Email;
            this.SaldoInicial = dto.SaldoInicial;
            this.DataCadastro = dto.DataCadastro;
        }
        #endregion

        #region Fábricas

        public static Usuario Cadastrar(UsuarioDTO dto, UsuarioValidator validator)
		{
            validator.ValidarCadastro(dto);
            return new Usuario(dto);
		}
    

        #endregion

        #region Operações

        #endregion

    }
}


