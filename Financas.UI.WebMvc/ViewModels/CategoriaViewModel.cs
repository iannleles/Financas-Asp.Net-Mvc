
namespace Financas.UI.WebMvc.ViewModels
{
    public class CategoriaViewModel
    {

        public int? CategoriaId { get; set; }

        public string CategoriaNome { get; set; }

        public string CategoriaTipo { get; set; }

        public string CategoriaTipoDescricao { get; set; }

        public int? UsuarioId { get; set; }

        public int? CategoriaPaiId { get;  set; }

        public string CategoriaPaiDescricao { get; set; }

    }
}