using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Domain.Dtos
{
    public class DashboardDiarioDTO
    {
        public DashboardDiarioTotalizacaoDTO DashboardDiarioTotalizacaoDTO { get; set; }
        public List<decimal> DashboardDiarioFluxoCaixaDTO { get; set; }
        public List<DashboardDiarioDespesasPaiDTO> DashboardDiarioDespesasPaiDTO { get; set; }
        public List<DashboardDiarioDespesasFilhoDTO> DashboardDiarioDespesasFilhoDTO { get; set; }
    }

    public class DashboardDiarioTotalizacaoDTO
    {
        public decimal SaldoInicial { get; set; }
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal SaldoAtual { get; set; }
    }

    public class DashboardDiarioDespesasPaiDTO
    {
        public int CategoriaIdPai { get; set; }
        public string CategoriaNomePai { get; set; }
        public decimal Total { get; set; }
    }

      public class DashboardDiarioDespesasFilhoDTO
    {
        public int CategoriaIdPai { get; set; }
        public string CategoriaNomePai { get; set; }
        public string CategoriaNomeFilho { get; set; }
        public decimal Total { get; set; }
    }

}



