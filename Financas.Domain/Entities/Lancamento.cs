using Financas.Domain.Dtos;
using Financas.Domain.Entidade.Validators;
using System;

namespace Financas.Domain.Entities
{
    public class Lancamento
    {

        #region Propriedades

        public int? Id { get; private set; }

        public DateTime Data { get; private set; }

        public decimal Valor { get; private set; }

        public string Descricao { get; private set; }

        public string Tipo { get; private set; }

        public int? CategoriaId { get; private set; }
        public virtual Categoria Categoria { get; private set; }


        public int? UsuarioId { get; private set; }
        public virtual Usuario Usuario { get; private set; }


        #endregion

        #region Construtores

        protected Lancamento()
		{

		}

        public Lancamento(LancamentoDTO dto)
		{
            this.Data = dto.Data ;
            this.Valor = dto.Valor;
            this.Descricao = dto.Descricao;
            this.Tipo = dto.Tipo;
            this.CategoriaId = dto.CategoriaId;
            this.UsuarioId = dto.UsuarioId;
		}

        #endregion

        #region Fábricas

        public static Lancamento Cadastrar(LancamentoDTO dto, LancamentoValidator validator)
        {

            validator.ValidarCadastro(dto);
            return new Lancamento(dto);

        }

        #endregion

        #region Operações

        public void Editar(LancamentoDTO dto, LancamentoValidator validator)
        {

            validator.ValidarEdicao(dto);

            this.Data = dto.Data;
            this.Valor = dto.Valor;
            this.Descricao = dto.Descricao;
            this.Tipo = dto.Tipo;
            this.CategoriaId = dto.CategoriaId;
            this.UsuarioId = dto.UsuarioId;

        }


        public void Excluir(LancamentoValidator validator)
        {
            LancamentoDTO dto = new LancamentoDTO
            {
                Id = this.Id,
                UsuarioId = this.UsuarioId
            };

            validator.ValidarExclusao(dto);
        }

        #endregion
    }
}
