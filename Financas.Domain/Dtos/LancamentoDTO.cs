
using System;
namespace Financas.Domain.Dtos
{
    public class LancamentoDTO
    {
        public int? Id { get; set; }

        public DateTime Data { get;  set; }

        public decimal Valor { get;  set; }

        public string Descricao { get; set; }

        public string Tipo { get; set; }

        public int? CategoriaId { get; set; }

        public int? UsuarioId { get;  set; }

    }
}
