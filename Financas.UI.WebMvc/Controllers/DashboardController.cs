using Financas.Domain.Contracts.Services;
using Financas.Domain.Dtos;
using Financas.Domain.Entities;
using Financas.UI.WebMvc.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using CustomAuthetication.Helpers;


namespace Financas.UI.WebMvc.Controllers
{
    [CustomAuthorize]
    public class DashboardController : BaseController
    {
       private readonly IDashboardAppService _dashboardAppService;
       private readonly IDespesaAppService _despesaAppService;

       public DashboardController(IDashboardAppService dashboardAppService, IDespesaAppService despesaAppService)
            : base()
        {
            this._dashboardAppService = dashboardAppService;
            this._despesaAppService = despesaAppService;
        }

        public ActionResult Home()
        {
            ViewBag.DespesasFixas = LoadDespesasFixas();
            return View(base.Usuario);
        }

        public ActionResult PrevistoRealizado()
        {
            ViewBag.DespesasFixas = LoadDespesasFixas();
            return View(base.Usuario);
        }

        public ActionResult ReceitasDespesasDiario()
        {
            return View(base.Usuario);
        }

        public ActionResult ReceitasDespesasMeses()
        {
            return View(base.Usuario);
        }

        public ActionResult ReceitasDespesasAnos()
        {
            return View(base.Usuario);
        }

        public JsonResult ObterDashboardHome(string ano, string mes)
        {
            try
            {

                DashboardHomeDTO dashboard = _dashboardAppService.ObterDashboardHome(base.Usuario.Id, int.Parse(ano), int.Parse(mes));

                DashboardHomeViewModel dashboardViewModel = new DashboardHomeViewModel();

                //Totalização
                dashboardViewModel.SaldoInicial = dashboard.DashboardHomeTotalizacaoDTO.SaldoInicial;
                dashboardViewModel.TotalDespesas = dashboard.DashboardHomeTotalizacaoDTO.TotalDespesas;
                dashboardViewModel.TotalReceitas = dashboard.DashboardHomeTotalizacaoDTO.TotalReceitas;
                dashboardViewModel.SaldoAtual = dashboard.DashboardHomeTotalizacaoDTO.SaldoAtual;

                //Despesas Pai e Filhos

                List<DespesasHomePaiViewModel> listaDespesasCategoriaPai = new List<DespesasHomePaiViewModel>();

                JArray listaDespesasCategoriaFilha = new JArray();

                foreach (var itemPai in dashboard.DashboardHomeDespesasPaiDTO.OrderByDescending(x => x.Total))
                {
                    listaDespesasCategoriaPai.Add(new DespesasHomePaiViewModel { name = itemPai.CategoriaNomePai, drilldown = itemPai.CategoriaNomePai, y = itemPai.Total });
                    dynamic jsonObject = new JObject();
                    jsonObject.name = itemPai.CategoriaNomePai;
                    jsonObject.id = itemPai.CategoriaNomePai;

                    JArray listaJArrayFilhas = new JArray();
                    foreach (var itemFilho in dashboard.DashboardHomeDespesasFilhoDTO.Where(x => x.CategoriaIdPai == itemPai.CategoriaIdPai))
                    {
                        JArray JArrayfilha = new JArray();
                        JArrayfilha.Add(itemFilho.CategoriaNomeFilho);
                        JArrayfilha.Add(itemFilho.Total);

                        listaJArrayFilhas.Add(JArrayfilha);
                    }

                    jsonObject.data = listaJArrayFilhas;

                    listaDespesasCategoriaFilha.Add(jsonObject);
                }

                dashboardViewModel.DespesasDiarioPaiViewModel = listaDespesasCategoriaPai;

                dashboardViewModel.DespesasFilhaViewModelString = listaDespesasCategoriaFilha.ToString();


                //Receitas e Despesas - Meses ano
                List<decimal> listaReceitas = new List<decimal>();

                List<decimal> listaDespesas = new List<decimal>();

                foreach (var item in dashboard.DashboardHomeMeses.OrderBy(o => o.Mes).ToList())
                {
                    listaReceitas.Add(item.Receita);
                    listaDespesas.Add(item.Despesa);
                }

                dashboardViewModel.ReceitasMeses = listaReceitas;

                dashboardViewModel.DespesasMeses = listaDespesas;

                return Json(dashboardViewModel, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(new { success = false, responseText = ex.InnerException }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult ObterDashboardPrevistoRealizado(string ano, string mes)
        {
            try
            {

                DashboardPrevistoRealizadoDTO dashboard = _dashboardAppService.ObterDashboardPrevistoRealizado(base.Usuario.Id, int.Parse(ano), int.Parse(mes));

                DashboardPrevistoRealizadoViewModel dashboardViewModel = new DashboardPrevistoRealizadoViewModel();


                //Receitas
                var receitas = dashboard.Categorias.Where(x => x.CategoriaTipoPai == "R");
                foreach (var item in receitas)
                {
                    dashboardViewModel.ReceitasPrevisto = item.Previsto;
                    dashboardViewModel.ReceitasRealizado = item.Realizado;
                }

                List<string> listaCategorias = new List<string>();
                List<decimal> listaDespesasPrevisto = new List<decimal>();
                List<decimal> listaDespesasRealizado = new List<decimal>();


                //Categorias e Despesas
                foreach (var item in dashboard.Categorias.Where(x => x.CategoriaTipoPai == "D").OrderByDescending(x => x.Realizado))
                {
                    listaCategorias.Add(item.CategoriaNomePai);
                    listaDespesasPrevisto.Add(item.Previsto);
                    listaDespesasRealizado.Add(item.Realizado);
                }

                dashboardViewModel.Categorias = listaCategorias;
                dashboardViewModel.DespesasPrevisto = listaDespesasPrevisto;
                dashboardViewModel.DespesasRealizado = listaDespesasRealizado;

                //Receitas Meses
                List<decimal> listaReceitas = new List<decimal>();
                List<decimal> listaDespesas = new List<decimal>();

                foreach (var item in dashboard.Meses.OrderBy(o => o.Mes).ToList())
                {
                    listaReceitas.Add(item.Receita);
                    listaDespesas.Add(item.Despesa);
                }

                dashboardViewModel.ReceitasMeses = listaReceitas;
                dashboardViewModel.DespesasMeses = listaDespesas;


                return Json(dashboardViewModel, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(new { success = false, responseText = ex.InnerException }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult ObterDashboardDiario(string ano, string mes)
        {
            try
            {

                DashboardDiarioDTO dashboard = _dashboardAppService.ObterDashboardDiario(base.Usuario.Id, int.Parse(ano), int.Parse(mes));

                DashboardDiarioViewModel dashboardViewModel = new DashboardDiarioViewModel();

                dashboardViewModel.SaldoInicial = dashboard.DashboardDiarioTotalizacaoDTO.SaldoInicial;
                dashboardViewModel.TotalDespesas = dashboard.DashboardDiarioTotalizacaoDTO.TotalDespesas;
                dashboardViewModel.TotalReceitas = dashboard.DashboardDiarioTotalizacaoDTO.TotalReceitas;
                dashboardViewModel.SaldoAtual = dashboard.DashboardDiarioTotalizacaoDTO.SaldoAtual;

                List<DespesasDiarioPaiViewModel> listaDespesasCategoriaPai = new List<DespesasDiarioPaiViewModel>();

                dashboardViewModel.FluxoCaixaDespesa = dashboard.DashboardDiarioFluxoCaixaDTO;

                JArray listaDespesasCategoriaFilha = new JArray();

                foreach (var itemPai in dashboard.DashboardDiarioDespesasPaiDTO.OrderByDescending(x => x.Total))
                {
                    listaDespesasCategoriaPai.Add(new DespesasDiarioPaiViewModel { name = itemPai.CategoriaNomePai, drilldown = itemPai.CategoriaNomePai, y = itemPai.Total });
                    dynamic jsonObject = new JObject();
                    jsonObject.name = itemPai.CategoriaNomePai;
                    jsonObject.id = itemPai.CategoriaNomePai;

                    JArray listaJArrayFilhas = new JArray();
                    foreach (var itemFilho in dashboard.DashboardDiarioDespesasFilhoDTO.Where(x => x.CategoriaIdPai == itemPai.CategoriaIdPai))
                    {
                        JArray JArrayfilha = new JArray();
                        JArrayfilha.Add(itemFilho.CategoriaNomeFilho);
                        JArrayfilha.Add(itemFilho.Total);

                        listaJArrayFilhas.Add(JArrayfilha);
                    }

                    jsonObject.data = listaJArrayFilhas;

                    listaDespesasCategoriaFilha.Add(jsonObject);
                }

                dashboardViewModel.DespesasDiarioPaiViewModel = listaDespesasCategoriaPai;

                dashboardViewModel.DespesasFilhaViewModelString = listaDespesasCategoriaFilha.ToString();

                return Json(dashboardViewModel, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(new { success = false, responseText = ex.InnerException }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult ObterDashboardMeses(string ano)
        {
            try
            {
                List<DashboardMesesDTO> dashboard = _dashboardAppService.ObterDashboardMeses(base.Usuario.Id, int.Parse(ano)).OrderBy(o => o.Mes).ToList();
                DashboardReceitaDespesaMesesViewModel dashboardViewModel = new DashboardReceitaDespesaMesesViewModel();

                List<decimal> listaReceitas = new List<decimal>();
                List<decimal> listaDespesas = new List<decimal>();


                foreach (var item in dashboard)
                {
                    listaReceitas.Add(item.Receita);
                    listaDespesas.Add(item.Despesa);
                }

                dashboardViewModel.Receitas = listaReceitas;
                dashboardViewModel.Despesas = listaDespesas;

                return Json(dashboardViewModel, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(new { success = false, responseText = ex.InnerException }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult ObterDashboardAnos(string anoInicial, string anoFinal)
        {
            try
            {
                List<DashboardAnosDTO> dashboard = _dashboardAppService.ObterDashboardAnos(base.Usuario.Id, int.Parse(anoInicial), int.Parse(anoFinal)).OrderBy(o => o.Ano).ToList();
                DashboardReceitaDespesaAnualViewModel dashboardViewModel = new DashboardReceitaDespesaAnualViewModel();
                List<int> listaAnos = new List<int>();
                List<decimal>listaReceitas = new List<decimal>();
                List<decimal> listaDespesas = new List<decimal>();


                foreach (var item in dashboard)
	            {
                    listaAnos.Add(item.Ano);
                    listaReceitas.Add(item.Receita);
                    listaDespesas.Add(item.Despesa);
	            }

                dashboardViewModel.Anos = listaAnos;
                dashboardViewModel.Receitas = listaReceitas;
                dashboardViewModel.Despesas = listaDespesas;

                return Json(dashboardViewModel, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(new { success = false, responseText = ex.InnerException }, JsonRequestBehavior.AllowGet);
            }

        }

        private List<DespesaFixaHomeViewModel> LoadDespesasFixas()
        {

            List<Despesa> listaDespesas = _despesaAppService.ListarDespesaFixas(base.Usuario.Id).OrderBy(x => x.Vencto).ToList();
            List<DespesaFixaHomeViewModel> listaDespesaViewModel = new List<DespesaFixaHomeViewModel>();

            foreach (var item in listaDespesas)
            {
                listaDespesaViewModel.Add(new DespesaFixaHomeViewModel
                {
                    DespesaVencto = item.Vencto,
                    DespesaValor = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", item.Valor),
                    CategoriaDescricao = item.CategoriaId.HasValue ? item.Categoria.Nome : ""
                });

            }

            return listaDespesaViewModel;
        }

    }
}