
using Financas.Domain.Dtos;
using Financas.Domain.Entidade.Validators;
namespace Financas.Domain.Entities
{
    public class Orcamento
    {
        #region Propriedades

        public int? Id { get; private set; }

        public string Tipo { get; private set; }

        public int Mes { get; private set; }

        public int Ano { get; private set; }

        public decimal Valor { get; private set; }

        public int? CategoriaId { get; private set; }
        public virtual Categoria Categoria { get; private set; }

        public int? UsuarioId { get; private set; }
        public virtual Usuario Usuario { get; private set; }

        #endregion

        #region Construtores

        protected Orcamento()
		{

		}

        public Orcamento(OrcamentoDTO dto)
		{
            this.Tipo = dto.Tipo;
            this.Mes = dto.Mes;
            this.Ano = dto.Ano;
            this.Valor = dto.Valor;
            this.CategoriaId = dto.CategoriaId;
            this.UsuarioId = dto.UsuarioId;
		}

        #endregion

        #region Fábricas

        public static Orcamento Cadastrar(OrcamentoDTO dto, OrcamentoValidator validator)
        {

            validator.ValidarCadastro(dto);
            return new Orcamento(dto);

        }

        #endregion

        #region Operações

        public void Editar(OrcamentoDTO dto, OrcamentoValidator validator)
        {

            validator.ValidarEdicao(dto);

            this.Tipo = dto.Tipo;
            this.Mes = dto.Mes;
            this.Ano = dto.Ano;
            this.Valor = dto.Valor;
            this.CategoriaId = dto.CategoriaId;
            this.UsuarioId = dto.UsuarioId;

        }


        public void Excluir(OrcamentoValidator validator)
        {
            OrcamentoDTO dto = new OrcamentoDTO
            {
                Id = this.Id,
                UsuarioId = this.UsuarioId
            };

            validator.ValidarExclusao(dto);
        }

        #endregion
    }
}
