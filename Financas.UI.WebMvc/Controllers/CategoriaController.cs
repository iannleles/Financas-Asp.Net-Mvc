using Financas.Domain.Contracts.Services;
using Financas.Domain.Dtos;
using Financas.Domain.Entities;
using Financas.Domain.ValueObjects;
using Financas.UI.WebMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CustomAuthetication.Helpers;
using Financas.UI.WebMvc.Helpers;

namespace Financas.UI.WebMvc.Controllers
{
    [CustomAuthorize]
    public class CategoriaController : BaseController
    {
        private readonly ICategoriaAppService _categoriaAppService;

        public CategoriaController(ICategoriaAppService categoriaAppService)
            : base()
        {
            this._categoriaAppService = categoriaAppService;
        }

        public ActionResult Index()
        {
            return View(base.Usuario);
        }

        public ActionResult Receitas()
        {
            Session["CategoriaCadastro"] = null;
            return View(base.Usuario);
        }

        public ActionResult Despesas()
        {
            Session["CategoriaCadastro"] = null;
            return View(base.Usuario);
        }


        public ActionResult Organizacao()
        {

            int? idUsuario = base.Usuario.Id;

            List<Categoria> listaCategorias = _categoriaAppService.ListarCategoriasPorUsuario(idUsuario).ToList();

            List<CategoriaViewModel> listaCategoriaViewModel = new List<CategoriaViewModel>();

            foreach (var item in listaCategorias)
            {
                listaCategoriaViewModel.Add(new CategoriaViewModel
                {
                    CategoriaId = item.Id,
                    CategoriaNome = item.Nome,
                    CategoriaTipo = item.Tipo,
                    CategoriaTipoDescricao = item.Tipo == "R" ? "Receita" : "Despesa",
                    UsuarioId = item.UsuarioId,
                    CategoriaPaiId = item.CategoriaPaiId.HasValue ? item.CategoriaPaiId : null,
                    CategoriaPaiDescricao = item.CategoriaPaiId.HasValue ? item.CategoriaPai.Nome : ""
                });
            }

            ViewBag.Categorias = listaCategoriaViewModel;

            return View(base.Usuario);
        }


        public ActionResult Processar(string _tipoCategoria)
        {
            string echo = Request.Params["sEcho"].ToString();
            string iColumns = Request.Params["iColumns"].ToString();
            string sColumns = Request.Params["sColumns"].ToString();
            int iDisplayStart = int.Parse(Request.Params["iDisplayStart"].ToString());
            int iDisplayLength = int.Parse(Request.Params["iDisplayLength"].ToString());
            string mDataProp_0 = Request.Params["mDataProp_0"].ToString();
            string sSearch = Request.Params["sSearch"].ToString();
            string iSortCol_0 = Request.Params["iSortCol_0"].ToString();
            string sSortDir_0 = Request.Params["sSortDir_0"].ToString();
            string iSortingCols = Request.Params["iSortingCols"].ToString();
            string bSortable_0 = Request.Params["bSortable_0"].ToString();
            int regExibir = iDisplayLength;
            int startExibir = iDisplayStart;
            string sortField = "CategoriaNome";

            if (iSortCol_0 == "0")
                sortField = "Nome";
            else if (iSortCol_0 == "1")
                sortField = "CategoriaPai";


            SortField[] sorts = new SortField[1];
            sorts[0] = new SortField(sortField, SortFieldDirection.Ascending);



            int page = (iDisplayStart / iDisplayLength) + 1;
            int pageSize = iDisplayLength;
            int recordCount;

            List<Categoria> listaCategorias = _categoriaAppService.ListarCategoriaPaginacao(out recordCount, _tipoCategoria, sSearch, base.Usuario.Id, sorts, pageSize, page).ToList();

            List<CategoriaViewModel> listaCategoriaViewModel = new List<CategoriaViewModel>();

            foreach (var item in listaCategorias)
            {
                listaCategoriaViewModel.Add(new CategoriaViewModel
                {
                    CategoriaId = item.Id,
                    CategoriaNome = item.Nome,
                    CategoriaTipo = item.Tipo,
                    CategoriaTipoDescricao = item.Tipo == "R" ? "Receita" : "Despesa",
                    UsuarioId = item.UsuarioId,
                    CategoriaPaiId = item.CategoriaPaiId.HasValue ? item.CategoriaPaiId : null,
                    CategoriaPaiDescricao = item.CategoriaPaiId.HasValue ? item.CategoriaPai.Nome : ""
                });
            }

            return Json(
                new
                {
                    iTotalRecords = recordCount,
                    iTotalDisplayRecords = recordCount,
                    sEcho = echo,
                    aaData = listaCategoriaViewModel
                }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SalvarCategoria(FormCollection formCadastro)
        {
            try
            {
                string retornoMensagem;
                string idCategoria = formCadastro["idCategoria"];
                string nomeCategoria = formCadastro["nomeCategoria"];
                string tipoCategoria = formCadastro["tipoCategoria"];
                string categoriaPrincipal = formCadastro["categoriaPrincipal"];


                CategoriaDTO dto = new CategoriaDTO();
                dto.Nome = nomeCategoria;
                dto.Tipo = tipoCategoria;
                dto.UsuarioId = base.Usuario.Id;
                if (!String.IsNullOrEmpty(idCategoria)) dto.Id = int.Parse(idCategoria);
                if (!String.IsNullOrEmpty(categoriaPrincipal)) dto.CategoriaPaiId = int.Parse(categoriaPrincipal);


                if (String.IsNullOrEmpty(idCategoria))
                {
                    _categoriaAppService.CadastrarCategoria(dto);
                    retornoMensagem = "registro incluído com sucesso!";
                }
                else
                {
                    _categoriaAppService.EditarCategoria(dto);
                    retornoMensagem = "registro atualizado com sucesso!";
                }

                new GerenciaSessionCategoria(_categoriaAppService).LoadCategoriaSession();
                CarregarCategoriaCadastroSession(tipoCategoria);

                return Json(new { success = true, responseText = retornoMensagem }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult ObterCategoria(int id)
        {
            try
            {

                Categoria categoria = _categoriaAppService.GetById(id);

                CategoriaViewModel categoriaViewModel = new CategoriaViewModel();
                categoriaViewModel.CategoriaId = categoria.Id;
                categoriaViewModel.CategoriaNome = categoria.Nome;
                categoriaViewModel.CategoriaTipo = categoria.Tipo;
                categoriaViewModel.CategoriaTipoDescricao = categoria.Tipo == "R" ? "Receita" : "Despesa";
                categoriaViewModel.UsuarioId = categoria.UsuarioId;
                categoriaViewModel.CategoriaPaiId = categoria.CategoriaPaiId.HasValue ? categoria.CategoriaPaiId : null;
                categoriaViewModel.CategoriaPaiDescricao = categoria.CategoriaPaiId.HasValue ? categoria.CategoriaPai.Nome : "";

                return Json(categoriaViewModel, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(new { success = false, responseText = ex.InnerException }, JsonRequestBehavior.AllowGet);
            }


        }


        public JsonResult ExcluirCategoria(int idCategoria, string tipoCategoria)
        {
            try
            {
                _categoriaAppService.ExcluirCategoria(idCategoria);

                new GerenciaSessionCategoria(_categoriaAppService).LoadCategoriaSession();
                CarregarCategoriaCadastroSession(tipoCategoria);

                return Json(new { success = true, responseText = "registro excluído com sucesso!" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(new { success = false, responseText = ex.Message.Replace("Validation failed:", "").Replace("--", "").Trim() }, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult ListarCategoriasPorTipo(string tipoCategoria)
        {
            List<CategoriaViewModel> listaCategoriaViewModel = new List<CategoriaViewModel>();

            if(Session["CategoriaCadastro"] != null)
            {
                listaCategoriaViewModel = (List<CategoriaViewModel>)Session["CategoriaCadastro"];
            }
            else
            {
                listaCategoriaViewModel = CarregarCategoriaCadastroSession(tipoCategoria);
                
            }

            return Json(listaCategoriaViewModel, JsonRequestBehavior.AllowGet);
        }


        private List<CategoriaViewModel> CarregarCategoriaCadastroSession(string tipoCategoria)
        {
            int? idUsuario = base.Usuario.Id;

            List<Categoria> listaCategorias = _categoriaAppService.ListarCategoriasPorTipo(tipoCategoria, base.Usuario.Id).OrderBy(x => x.Nome).ToList();
            List<CategoriaViewModel> listaCategoriaViewModel = new List<CategoriaViewModel>();

            foreach (var item in listaCategorias)
            {
                listaCategoriaViewModel.Add(new CategoriaViewModel
                {
                    CategoriaId = item.Id,
                    CategoriaNome = item.Nome
                });
            }

            GerenciaSessionBase.Atualiza("CategoriaCadastro", listaCategoriaViewModel); 

            return listaCategoriaViewModel;

        }

    }
}