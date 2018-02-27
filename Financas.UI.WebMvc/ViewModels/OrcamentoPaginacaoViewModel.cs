using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Financas.UI.WebMvc.ViewModels
{
    public class OrcamentoPaginacaoViewModel
    {
        public int? Id { get; set; }

        public string Categoria { get; set; }

        public decimal Receita { get; set; }

        public decimal Despesa { get; set; }

    }
}


