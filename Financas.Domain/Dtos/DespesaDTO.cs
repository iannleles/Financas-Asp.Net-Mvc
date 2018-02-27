
namespace Financas.Domain.Dtos
{
    public class DespesaDTO
    {
        public int? Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int Vencto { get; set; }
        public int? UsuarioId { get; set; }
        public int? CategoriaId { get; set; }
    }
}


