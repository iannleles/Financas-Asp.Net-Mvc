using System.Web.Mvc;
using CustomAuthetication.Helpers;
using Financas.Domain.Contracts.Services;

namespace Financas.UI.WebMvc.Controllers
{
    [CustomAuthorize]
    public class HomeController : BaseController
    {

        public HomeController()
            : base()
        {

        }

        public ActionResult Index()
        {
            return View(base.Usuario);
        }

    }
}