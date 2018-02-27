using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Financas.UI.WebMvc.ViewModels
{
    public class DashboardPrevistoRealizadoViewModel
    {
        public List<string> Categorias { get; set; }

        public decimal ReceitasPrevisto { get; set; }
        public decimal ReceitasRealizado { get; set; }

        public List<decimal> DespesasPrevisto { get; set; }
        public List<decimal> DespesasRealizado { get; set; }

        public List<decimal> ReceitasMeses { get; set; }
        public List<decimal> DespesasMeses { get; set; }

    }
}


