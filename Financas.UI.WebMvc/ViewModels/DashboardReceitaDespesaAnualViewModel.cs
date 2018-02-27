using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Financas.UI.WebMvc.ViewModels
{
    public class DashboardReceitaDespesaAnualViewModel
    {
        public List<int> Anos { get; set; }
        public List<decimal> Receitas { get; set; }
        public List<decimal> Despesas { get; set; }

    }
}