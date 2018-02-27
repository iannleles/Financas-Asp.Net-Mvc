using System.Web.Mvc;
using Financas.Domain.Contracts.Services;
using Financas.Domain.Dtos;
using Financas.Domain.Entities;

namespace Financas.UI.WebMvc.Controllers
{
    public abstract class BaseController : Controller
    {

        public BaseController()
        {
          
        }

        public UsuarioDTO Usuario
        {
            get
            {
                return (UsuarioDTO)Session["Usuario"];
            }
        }
    }
}