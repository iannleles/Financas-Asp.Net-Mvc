using System.Collections.Generic;

namespace Financas.UI.WebMvc.ViewModels
{
    public class CategoriaAgrupadasViewModel
    {

        public int? CategoriaPaiId { get; set; }

        public string CategoriaPaiNome { get; set; }

        public List<CategoriaViewModel> CategoriaFilha { get; set; }

    }
}