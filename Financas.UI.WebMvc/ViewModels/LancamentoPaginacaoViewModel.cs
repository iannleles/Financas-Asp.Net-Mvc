
using System;
namespace Financas.UI.WebMvc.ViewModels
{
    public class LancamentoPaginacaoViewModel
    {

        public int? Id { get; set; }

        public string Data { get; set; }

        public string Descricao { get; set; }

        public decimal Entrada { get; set; }

        public decimal Saida { get; set; }

        public decimal Saldo { get; set; }


    }
}