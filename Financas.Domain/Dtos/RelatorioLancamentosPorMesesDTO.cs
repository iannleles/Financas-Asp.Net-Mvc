using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Domain.Dtos
{
    public class RelatorioLancamentosPorMesesDTO
    {
        public List<RelatorioLancamentosPorMesesPaiDTO> RelatorioLancamentosPorMesesPaiDTO { get; set; }

        public List<RelatorioLancamentosPorMesesFilhoDTO> RelatorioLancamentosPorMesesFilhoDTO { get; set; }

    }

    public class RelatorioLancamentosPorMesesPaiDTO
    {
        public int CategoriaIdPai { get; set; }

        public string CategoriaTipoPai { get; set; }

        public string CategoriaNomePai { get; set; }

        public int CategoriaIdFilho { get; set; }

        public string CategoriaNomeFilho { get; set; }



    }

    public class RelatorioLancamentosPorMesesFilhoDTO
    {

        public int CategoriaIdFilho { get; set; }

        public int Mes { get; set; }

        public decimal Total { get; set; }



    }
           
  
}



