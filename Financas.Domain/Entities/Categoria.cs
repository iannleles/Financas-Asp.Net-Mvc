using System.Collections.Generic;
using Financas.Domain.Dtos;
using Financas.Domain.Entidade.Validators;
using FluentValidation;

namespace Financas.Domain.Entities
{
    public class Categoria 
    {

        #region Propriedades

        public int? Id { get; private set; }

        public string Nome { get; private set; }

        public string Tipo { get; private set; }


        public int? UsuarioId { get; private set; }
        public virtual Usuario Usuario { get; private set; }

        public int? CategoriaPaiId { get; private set; }
        public virtual Categoria CategoriaPai { get; private set; }

        public virtual ICollection<Categoria> CategoriaFilhas { get; set; }

        public virtual ICollection<Despesa> Despesas { get; set; }

        public virtual ICollection<Lancamento> Lancamentos { get; set; }

        public virtual ICollection<Orcamento> Orcamentos { get; private set; }


        #endregion

        #region Construtores

        protected Categoria()
		{
            CategoriaFilhas = new HashSet<Categoria>();
            Despesas = new HashSet<Despesa>();
            Lancamentos = new HashSet<Lancamento>();
            Orcamentos = new HashSet<Orcamento>();
		}

        private Categoria(CategoriaDTO dto)
        {
            this.Nome = dto.Nome;
            this.Tipo = dto.Tipo;
            this.UsuarioId = dto.UsuarioId;
            this.CategoriaPaiId = dto.CategoriaPaiId;
        }

        #endregion

        #region Fábricas

        public static Categoria Cadastrar(CategoriaDTO dto, CategoriaValidator validator)
        {

            validator.ValidarCadastro(dto);
            return new Categoria(dto);

        }

        #endregion

        #region Operações

        public void Editar(CategoriaDTO dto, CategoriaValidator validator)
        {

            validator.ValidarEdicao(dto);

            this.Nome = dto.Nome;
            this.Tipo = dto.Tipo;
            this.UsuarioId = dto.UsuarioId;
            this.CategoriaPaiId = dto.CategoriaPaiId;

        }


        public void Excluir(CategoriaValidator validator)
        {
            CategoriaDTO dto = new CategoriaDTO
            {
                Id = this.Id,
                UsuarioId = this.UsuarioId
            };

            validator.ValidarExclusao(dto);
        }

        #endregion

    }
}
