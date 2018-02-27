using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Domain.Dtos
{
    public class RelatorioLancamentosPorPeriodoDTO
    {
        public DateTime Data { get; set; }

        public string Descricao { get; set; }

        public decimal Entrada { get; set; }

        public decimal Saida { get; set; }

    }
}
