using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Financas.UI.WebMvc.ViewModels
{
    public class DashboardDiarioViewModel
    {
        public decimal SaldoInicial { get; set; }
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal SaldoAtual { get; set; }

        public List<decimal> FluxoCaixaDespesa { get; set; }

        public List<DespesasDiarioPaiViewModel> DespesasDiarioPaiViewModel { get; set; }

        public string DespesasFilhaViewModelString { get; set; }

    }


    public class DespesasDiarioPaiViewModel
    {
        public string name { get; set; }
        public decimal y { get; set; }
        public string drilldown { get; set; }
    }

}




