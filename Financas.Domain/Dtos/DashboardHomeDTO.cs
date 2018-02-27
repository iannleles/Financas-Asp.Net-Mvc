using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Domain.Dtos
{
    public class DashboardHomeDTO
    {
        public DashboardHomeTotalizacaoDTO DashboardHomeTotalizacaoDTO { get; set; }
        public List<DashboardHomeDespesasPaiDTO> DashboardHomeDespesasPaiDTO { get; set; }
        public List<DashboardHomeDespesasFilhoDTO> DashboardHomeDespesasFilhoDTO { get; set; }
        public List<DashboardHomeMesesDTO> DashboardHomeMeses { get; set; }
    }

    public class DashboardHomeTotalizacaoDTO
    {
        public decimal SaldoInicial { get; set; }
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal SaldoAtual { get; set; }
    }

    public class DashboardHomeDespesasPaiDTO
    {
        public int CategoriaIdPai { get; set; }
        public string CategoriaNomePai { get; set; }
        public decimal Total { get; set; }
    }

    public class DashboardHomeDespesasFilhoDTO
    {
        public int CategoriaIdPai { get; set; }
        public string CategoriaNomePai { get; set; }
        public string CategoriaNomeFilho { get; set; }
        public decimal Total { get; set; }
    }

    public class DashboardHomeMesesDTO
    {
        public int Mes { get; set; }
        public decimal Receita { get; set; }
        public decimal Despesa { get; set; }
    }

}



