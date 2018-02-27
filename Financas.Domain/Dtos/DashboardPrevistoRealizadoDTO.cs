using System.Collections.Generic;

namespace Financas.Domain.Dtos
{
    public class DashboardPrevistoRealizadoDTO
    {
        public List<DashboardHomePrevistoRealizadoCategoriaDTO> Categorias { get; set; }
        public List<DashboardPrevistoRealizadoMesesDTO> Meses { get; set; }
    }

    public class DashboardHomePrevistoRealizadoCategoriaDTO
    {
        public int CategoriaIdPai { get; set; }
        public string CategoriaNomePai { get; set; }
        public string CategoriaTipoPai { get; set; }
        public decimal Previsto { get; set; }
        public decimal Realizado { get; set; }
    }

    public class DashboardPrevistoRealizadoMesesDTO
    {
        public int Mes { get; set; }
        public decimal Receita { get; set; }
        public decimal Despesa { get; set; }
    }

}



