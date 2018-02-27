using System;
using System.Web.Mvc;
using System.Web.Security;
using Financas.Domain.Contracts.Services;
using Financas.Domain.Dtos;
using Financas.UI.WebMvc.ViewModels;
using Financas.Domain.Entities;
using System.Linq;
using System.Collections.Generic;
using Financas.UI.WebMvc.Helpers;

namespace Financas.UI.WebMvc.Controllers
{
    public class AccountController : Controller
    {
        private IAutenticacaoAppService _autenticacaoAppService;
        private readonly ICategoriaAppService _categoriaAppService;

        public AccountController(IAutenticacaoAppService autenticacaoAppService, ICategoriaAppService categoriaAppService)
        {
            this._autenticacaoAppService = autenticacaoAppService;
            this._categoriaAppService = categoriaAppService;

        }


        public ActionResult Index()
        {
            return View();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(string email, string senha)
        {

            try
            {
                if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(senha))
                {
                    return Json(new { success = false, responseText = "Login Inválido!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    UsuarioDTO usuario = _autenticacaoAppService.AutenticarUsuario(email, senha);

                    if (usuario != null)
                    {

                        GerenciaSessionUsuario.Usuario = usuario;
                        new GerenciaSessionCategoria(_categoriaAppService).LoadCategoriaSession();

                        FormsAuthentication.SetAuthCookie(usuario.Nome,true);

                        return Json(new { success = true, responseText = "Autenticação realizada com sucesso!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Session["Usuario"] = null;
                        return Json(new { success = false, responseText = "Login Inválido!" }, JsonRequestBehavior.AllowGet);
                    }
                }

            }

            catch (Exception ex)
            {
                throw ex;

            }

        }





        public ActionResult Logout()
        {
            GerenciaSessionBase.Abandon();
            //Session["Usuario"] = null;
            FormsAuthentication.SignOut();
            return Redirect(Url.Content("~/Login"));
        }
    }
}