using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Domain.Dtos
{
    public class DashboardDTO
    {
        public DashboardTotalizacaoDTO DashboardTotalizacao { get; set; }

        public List<decimal> FluxoCaixa { get; set; }

        public List<DashboardDespesasPaiDTO> DespesasCategoriaPai { get; set; }

        public List<DashboardDespesasFilhaDTO> DespesasCategoriaFilha { get; set; }

    }

    public class DashboardTotalizacaoDTO
    {
        public decimal SaldoInicial { get; set; }
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal SaldoAtual { get; set; }
    }

    public class DashboardDespesasPaiDTO
    {
        public int CategoriaId { get; set; }
        public string CategoriaNome { get; set; }
        public decimal Total { get; set; }
    }


    public class DashboardDespesasFilhaDTO
    {
        public int CategoriaPaiId { get; set; }
        public string CategoriaPaiNome { get; set; }

        public List<DashboardCategoriaFilhaDTO> Filhas { get; set; }
    }

    public class DashboardCategoriaFilhaDTO {

        public string Nome { get; set; }

        public decimal Total { get; set; }
    }


    public class DashboardDespesasPaiSPDTO
    {
        public string CategoriaNome { get; set; }
        public decimal Total { get; set; }
    }
}


