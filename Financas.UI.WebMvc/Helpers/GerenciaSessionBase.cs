using Financas.Domain.Contracts.Services;
using Financas.Domain.Entities;
using Financas.UI.WebMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Financas.UI.WebMvc.Helpers
{
    public class GerenciaSessionBase
    {

        public static T Le<T>(string nomeSession)
        {
            return (T)HttpContext.Current.Session[nomeSession];
        }

        public static T LeComDefault<T>(string nomeSession)
        {
            object valor = HttpContext.Current.Session[nomeSession];

            return valor == null ? default(T) : ((T)valor);
        }

        public static void Atualiza(string nomeSession, object valor)
        {
            HttpContext.Current.Session[nomeSession] = valor;
        }


        public static void Abandon()
        {
            HttpContext.Current.Session.Abandon();
        }

        public  static void RemoveAll()
        {
            HttpContext.Current.Session.RemoveAll();
        }

    }
}
