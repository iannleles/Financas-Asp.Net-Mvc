using System.Collections.Generic;

namespace Financas.UI.WebMvc.ViewModels
{
    public class DashboardHomeViewModel
    {
        public decimal SaldoInicial { get; set; }

        public decimal TotalReceitas { get; set; }

        public decimal TotalDespesas { get; set; }

        public decimal SaldoAtual { get; set; }

        public List<decimal> ReceitasMeses { get; set; }

        public List<decimal> DespesasMeses { get; set; }

        public List<DespesasHomePaiViewModel> DespesasDiarioPaiViewModel { get; set; }

        public string DespesasFilhaViewModelString { get; set; }

    }

    public class DespesasHomePaiViewModel
    {
        public string name { get; set; }
        public decimal y { get; set; }
        public string drilldown { get; set; }
    }
}