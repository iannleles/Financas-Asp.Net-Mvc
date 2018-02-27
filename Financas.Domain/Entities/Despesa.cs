using Financas.Domain.Dtos;
using Financas.Domain.Entidade.Validators;
using System;

namespace Financas.Domain.Entities
{
    public class Despesa 
    {
        #region Propriedades

        public int? Id { get; private set; }

        public string Descricao { get; private set; }

        public decimal Valor { get; private set; }

        public int Vencto { get; private set; }

        public int? UsuarioId { get; private set; }
        public virtual Usuario Usuario { get; private set; }

        public int? CategoriaId { get; private set; }
        public virtual Categoria Categoria { get; private set; }


        #endregion

        #region Construtores

        protected Despesa()
		{

		}

        public Despesa(DespesaDTO dto)
		{
            this.Descricao = dto.Descricao;
            this.Valor = dto.Valor;
            this.Vencto = dto.Vencto;
            this.UsuarioId = dto.UsuarioId;
            this.CategoriaId = dto.CategoriaId;

		}

        #endregion

        #region Fábricas

        public static Despesa Cadastrar(DespesaDTO dto, DespesaValidator validator)
        {

            validator.ValidarCadastro(dto);
            return new Despesa(dto);

        }

        #endregion

        #region Operações

        public void Editar(DespesaDTO dto, DespesaValidator validator)
        {

            validator.ValidarEdicao(dto);

            this.Descricao = dto.Descricao;
            this.Valor = dto.Valor;
            this.Vencto = dto.Vencto;
            this.UsuarioId = dto.UsuarioId;
            this.CategoriaId = dto.CategoriaId;

        }


        public void Excluir(DespesaValidator validator)
        {
            DespesaDTO dto = new DespesaDTO
            {
                Id = this.Id,
                UsuarioId = this.UsuarioId
            };

            validator.ValidarExclusao(dto);
        }

        #endregion

    }
}
