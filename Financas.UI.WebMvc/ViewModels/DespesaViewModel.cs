using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Financas.UI.WebMvc.ViewModels
{
    public class DespesaViewModel
    {
        public int? DespesaId { get; set; }

        public string DespesaDescricao { get; set; }

        public decimal DespesaValor { get; set; }

        public int DespesaVencto { get; set; }

        public int? UsuarioId { get; set; }

        public int? CategoriaId { get; set; }

        public string CategoriaDescricao { get; set; }
    }
}


