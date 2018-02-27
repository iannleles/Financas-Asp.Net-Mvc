
namespace Financas.Domain.Dtos
{
    public class CategoriaDTO
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public int? UsuarioId { get; set; }
        public int? CategoriaPaiId { get; set; }
        public string CategoriaPaiDescricao { get; set; }
    }
}
