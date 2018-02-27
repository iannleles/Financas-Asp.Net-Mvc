using System;
using System.Web;
using Financas.Domain.Contracts.Services;
using Financas.Domain.Entities;

namespace Financas.Application.Services
{
    public class AutenticacaoAppService : IAutenticacaoAppService
    {
        private readonly IUsuarioAppService _UsuarioAppService;

        public AutenticacaoAppService(IUsuarioAppService UsuarioAppService)
        {
            _UsuarioAppService = UsuarioAppService;
        }

        public bool AutenticarUsuario(string _email, string _senha)
        {

            try
            {
                string senhaCriptografa = CriptografiaAppService.Criptografar(_senha);
                Usuario usuario = _UsuarioAppService.GetByEmailSenha(_email, senhaCriptografa);

                if (usuario == null)
                {
                    return false;
                }
                else
                {

                    System.Web.Security.FormsAuthentication.SetAuthCookie(usuario.Id.ToString(), false);

                    //Criando um objeto cookie
                    HttpCookie UserCookie = new HttpCookie("UserCookieAuthentication");

                    //Setando o ID do usuário no cookie
                    UserCookie.Value = usuario.Id.ToString();

                    //Definindo o prazo de vida do cookie para um dia
                    //UserCookie.Expires = DateTime.Now.AddDays(1); 

                    //Adicionando o cookie no contexto da aplicação
                    HttpContext.Current.Response.Cookies.Add(UserCookie);

                    return true;

                }
            }
            catch (Exception ex)
            {
                // todo: logar
                throw;
            }
        }


        public Usuario ObterUsuarioLogado()
        {

            var Usuario = HttpContext.Current.Request.Cookies["UserCookieAuthentication"];
            if (Usuario == null)
            {
                return null;
            }
            else
            {
                string NovoToken = Usuario.Value.ToString();

                int IDUsuario;

                if (int.TryParse(NovoToken, out IDUsuario))
                {
                    return GetUsuarioByID(IDUsuario);
                }
                else
                {
                    return null;
                }
            }

        }

        //Recuperando o usuário pelo ID
        public Usuario GetUsuarioByID(int CodigoUsuario)
        {

            return _UsuarioAppService.GetById(CodigoUsuario);

        }

        public void Sair()
        {

            System.Web.Security.FormsAuthentication.SignOut();



            HttpCookie currentUserCookie = new HttpCookie("UserCookieAuthentication");
            currentUserCookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(currentUserCookie);


            System.Web.Security.FormsAuthentication.RedirectToLoginPage();

        }


    }
}
