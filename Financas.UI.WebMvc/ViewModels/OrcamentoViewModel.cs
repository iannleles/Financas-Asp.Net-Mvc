using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Financas.UI.WebMvc.ViewModels
{
    public class OrcamentoViewModel
    {
        public int? OrcamentoId { get; set; }

        public string OrcamentoTipo { get; set; }

        public int OrcamentoMes { get; set; }

        public int OrcamentoAno { get; set; }

        public decimal OrcamentoValor { get; set; }

        public int? OrcamentoCategoriaId { get; set; }

    }
}