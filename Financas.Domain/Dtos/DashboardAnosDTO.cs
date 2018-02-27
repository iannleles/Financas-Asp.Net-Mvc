using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Domain.Dtos
{
    public class DashboardAnosDTO
    {
        public int Ano { get; set; }
        public decimal Receita { get; set; }
        public decimal Despesa { get; set; }
    }
}
