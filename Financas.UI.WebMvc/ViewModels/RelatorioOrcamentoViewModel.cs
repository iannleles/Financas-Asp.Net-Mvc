using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Financas.UI.WebMvc.ViewModels
{
    public class RelatorioOrcamentoViewModel
    {
        public int CategoriaPaiId { get; set; }

        public string CategoriaPaiTipo { get; set; }

        public string CategoriaPaiNome { get; set; }

        public string CategoriaNome { get; set; }

        public decimal Previsto { get; set; }

        public decimal Realizado { get; set; }

        public decimal Diferenca { get; set; }
    }


}