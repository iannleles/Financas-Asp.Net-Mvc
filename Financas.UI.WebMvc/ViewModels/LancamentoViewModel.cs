using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Financas.UI.WebMvc.ViewModels
{
    public class LancamentoViewModel
    {
        public int? LancamentoId { get; set; }

        public string LancamentoData { get; set; }

        public decimal LancamentoValor { get; set; }

        public string LancamentoDescricao { get; set; }

        public string LancamentoTipo { get; set; }

        public int? LancamentoCategoriaId { get; set; }

    }
}