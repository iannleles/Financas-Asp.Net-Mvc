using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Financas.UI.WebMvc.ViewModels
{
    public class RelatorioLancamentoViewModel
    {
        public int CategoriaPaiId { get; set; }

        public string CategoriaPaiTipo { get; set; }

        public string CategoriaPaiNome { get; set; }

        public string CategoriaNome { get; set; }

        public decimal Jan { get; set; }

        public decimal Fev { get; set; }

        public decimal Mar { get; set; }

        public decimal Abr { get; set; }

        public decimal Mai { get; set; }

        public decimal Jun { get; set; }

        public decimal Jul { get; set; }

        public decimal Ago { get; set; }

        public decimal Set { get; set; }

        public decimal Out { get; set; }

        public decimal Nov { get; set; }

        public decimal Dez { get; set; }

        public decimal Total { get; set; }

    }


}