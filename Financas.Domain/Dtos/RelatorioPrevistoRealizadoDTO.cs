using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Domain.Dtos
{
    public class RelatorioPrevistoRealizadoDTO
    {
        public int CategoriaIdPai { get; set; }

        public string CategoriaTipoPai { get; set; }

        public string CategoriaNomePai { get; set; }

        public int CategoriaIdFilho { get; set; }

        public string CategoriaNomeFilho { get; set; }

        public decimal Previsto { get; set; }

        public decimal Realizado { get; set; }

        public decimal Diferenca { get; set; }
    }
}
