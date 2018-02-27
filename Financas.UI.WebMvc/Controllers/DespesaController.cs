using Financas.Domain.Contracts.Services;
using Financas.Domain.Dtos;
using Financas.Domain.Entities;
using Financas.Domain.ValueObjects;
using Financas.UI.WebMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using CustomAuthetication.Helpers;

namespace Financas.UI.WebMvc.Controllers
{
    [CustomAuthorize]
    public class DespesaController : BaseController
    {
       private readonly IDespesaAppService _despesaAppService;
       private readonly ICategoriaAppService _categoriaAppService;

       public DespesaController(IDespesaAppService despesaAppService, ICategoriaAppService categoriaAppService)
            : base()
        {
          this._despesaAppService = despesaAppService;
          this._categoriaAppService = categoriaAppService;
        }

        public ActionResult Index()
        {
            LoadCategorias();
            return View(base.Usuario);
        }

        public void LoadCategorias()
        {
            int? idUsuario = base.Usuario.Id;


            List<Categoria> listaCategorias = _categoriaAppService.ListarCategoriasPorUsuario(idUsuario).ToList();

            List<Categoria> listaCategoriasReceitaPai = listaCategorias.Where(c => c.Tipo == "R" && c.CategoriaPaiId == null).OrderBy(o => o.Nome).ToList();

            List<CategoriaAgrupadasViewModel> listaCategoriasReceitaAgrupadasViewModel = new List<CategoriaAgrupadasViewModel>();

            foreach (var item in listaCategoriasReceitaPai)
            {
                List<CategoriaViewModel> listaCategoriaFilha = new List<CategoriaViewModel>();
                foreach (var filha in item.CategoriaFilhas.OrderBy(o => o.Nome))
                {
                    listaCategoriaFilha.Add(new CategoriaViewModel { CategoriaId = filha.Id, CategoriaNome = filha.Nome });
                }

                listaCategoriasReceitaAgrupadasViewModel.Add(new CategoriaAgrupadasViewModel
                {
                    CategoriaPaiId = item.Id,
                    CategoriaPaiNome = item.Nome,
                    CategoriaFilha = listaCategoriaFilha
                });
            }

            List<Categoria> listaCategoriasDespesaPai = listaCategorias.Where(c => c.Tipo == "D" && c.CategoriaPaiId == null).OrderBy(o => o.Nome).ToList();

            List<CategoriaAgrupadasViewModel> listaCategoriaDespesaAgrupadasViewModel = new List<CategoriaAgrupadasViewModel>();

            foreach (var item in listaCategoriasDespesaPai)
            {
                List<CategoriaViewModel> listaCategoriaFilha = new List<CategoriaViewModel>();
                foreach (var filha in item.CategoriaFilhas.OrderBy(o => o.Nome))
                {
                    listaCategoriaFilha.Add(new CategoriaViewModel { CategoriaId = filha.Id, CategoriaNome = filha.Nome });
                }

                listaCategoriaDespesaAgrupadasViewModel.Add(new CategoriaAgrupadasViewModel
                {
                    CategoriaPaiId = item.Id,
                    CategoriaPaiNome = item.Nome,
                    CategoriaFilha = listaCategoriaFilha
                });
            }

            ViewBag.listaCategoriasReceita = listaCategoriasReceitaAgrupadasViewModel;
            ViewBag.listaCategoriasDespesa = listaCategoriaDespesaAgrupadasViewModel;

        }

        public ActionResult Processar()
        {
            decimal totalDespesasFixas = 0;
            int recordCount = 0;

            List<Despesa> listaDespesas = _despesaAppService.ListarDespesaFixas(base.Usuario.Id).OrderBy(x => x.Vencto).ToList();
            List<DespesaViewModel> listaDespesaViewModel = new List<DespesaViewModel>();

            foreach (var item in listaDespesas)
            {
                listaDespesaViewModel.Add(new DespesaViewModel
                {
                    DespesaId = item.Id,
                    DespesaDescricao = item.Descricao,
                    DespesaVencto = item.Vencto,
                    DespesaValor = item.Valor,
                    UsuarioId = item.UsuarioId,
                    CategoriaId = item.CategoriaId.HasValue ? item.CategoriaId : null,
                    CategoriaDescricao = item.CategoriaId.HasValue ? item.Categoria.Nome : ""
                });

                totalDespesasFixas = totalDespesasFixas + item.Valor;
            }

            recordCount = listaDespesas.Count();

            return Json(
             new
             {
                 iTotalRecords = recordCount,
                 iTotalDisplayRecords = recordCount,
                 aaData = listaDespesaViewModel,
                 iTotalDespesasFixas = totalDespesasFixas
             }, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public JsonResult SalvarDespesa(FormCollection formCadastro)
        {
            try
            {
                string retornoMensagem;
                string idDespesa = formCadastro["idDespesa"];
                string descricaoDespesa = formCadastro["descricaoDespesa"];
                string venctoDespesa = formCadastro["venctoDespesa"];
                string valorDespesa = formCadastro["valorDespesa"];
                string categoria = formCadastro["categoriaDespesa"];



                DespesaDTO dto = new DespesaDTO();
                dto.Descricao = descricaoDespesa;
                dto.Valor = Decimal.Parse(valorDespesa, NumberStyles.Currency, new CultureInfo("pt-BR"));
                dto.Vencto = int.Parse(venctoDespesa);
                dto.UsuarioId = base.Usuario.Id;
                if (!String.IsNullOrEmpty(idDespesa)) dto.Id = int.Parse(idDespesa);
                if (!String.IsNullOrEmpty(categoria)) dto.CategoriaId = int.Parse(categoria);


                if (String.IsNullOrEmpty(idDespesa))
                {
                    _despesaAppService.CadastrarDespesa(dto);
                    retornoMensagem = "registro incluído com sucesso!";
                }
                else
                {
                    _despesaAppService.EditarDespesa(dto);
                    retornoMensagem = "registro atualizado com sucesso!";
                }


                return Json(new { success = true, responseText = retornoMensagem }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult ObterDespesa(int id)
        {
            try
            {

                Despesa despesa = _despesaAppService.GetById(id);

                DespesaViewModel despesaViewModel = new DespesaViewModel();
                despesaViewModel.DespesaId = despesa.Id;
                despesaViewModel.DespesaDescricao = despesa.Descricao;
                despesaViewModel.DespesaVencto = despesa.Vencto;
                despesaViewModel.DespesaValor = despesa.Valor;
                despesaViewModel.UsuarioId = despesa.UsuarioId;
                despesaViewModel.CategoriaId = despesa.CategoriaId.HasValue ? despesa.CategoriaId : null;
                despesaViewModel.CategoriaDescricao = despesa.CategoriaId.HasValue ? despesa.Categoria.Nome : "";

                return Json(despesaViewModel, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(new { success = false, responseText = ex.InnerException }, JsonRequestBehavior.AllowGet);
            }


        }


        public JsonResult ExcluirDespesa(int idDespesa)
        {
            try
            {
                _despesaAppService.ExcluirDespesa(idDespesa);

                return Json(new { success = true, responseText = "registro excluído com sucesso!" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(new { success = false, responseText = ex.Message.Replace("Validation failed:", "").Replace("--", "").Trim() }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}