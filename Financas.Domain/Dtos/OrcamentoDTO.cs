using System;
namespace Financas.Domain.Dtos
{
    public class OrcamentoDTO
    {
        public int? Id { get; set; }

        public string Tipo { get; set; }

        public int Mes { get; set; }

        public int Ano { get; set; }

        public decimal Valor { get; set; }

        public int? CategoriaId { get; set; }

        public int? UsuarioId { get;  set; }

    }

}

