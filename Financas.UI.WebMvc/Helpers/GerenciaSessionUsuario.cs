using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Financas.UI.WebMvc.Helpers;
using Financas.Domain.Dtos;

namespace Financas.UI.WebMvc.Helpers
{
    public class GerenciaSessionUsuario : GerenciaSessionBase
    {
        private const string nomeSessionUsuario = "Usuario";

        public static UsuarioDTO Usuario
        {
            get
            {
                return LeComDefault<UsuarioDTO>(nomeSessionUsuario);
            }
            set
            {
                Atualiza(nomeSessionUsuario, value);
            }
        }
    }
}