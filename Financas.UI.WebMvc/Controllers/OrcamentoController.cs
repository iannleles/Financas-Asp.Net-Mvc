using Financas.Domain.Contracts.Services;
using Financas.Domain.Dtos;
using Financas.Domain.Entities;
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
    public class OrcamentoController : BaseController
    {
        private readonly IOrcamentoAppService _orcamentoAppService;

        public OrcamentoController(IOrcamentoAppService orcamentoAppService)
            : base()
        {
            this._orcamentoAppService = orcamentoAppService;
        }

        public ActionResult Index()
        {
            return View(base.Usuario);
        }

        public ActionResult Processar(int ano, int mes)
        {
            decimal totalReceitas = 0;
            decimal totalDespesas = 0;
            decimal saldoFinal = 0;

            CultureInfo cultureInfo = new CultureInfo("pt-BR");

            DateTime dataInicial = DateTime.Parse(string.Format("1/{0}/{1}", mes, ano), cultureInfo);

            List<Orcamento> listaOrcamentos = _orcamentoAppService.OrcamentosPorUsuarioAnoMes(base.Usuario.Id, ano, mes).OrderBy(o => o.Categoria.Nome).ToList();

            List<OrcamentoPaginacaoViewModel> listaOrcamentoPaginacaoViewModel = new List<OrcamentoPaginacaoViewModel>();

            foreach (var item in listaOrcamentos)
            {
                OrcamentoPaginacaoViewModel orcamentoPaginacaoViewModel = new OrcamentoPaginacaoViewModel();

                orcamentoPaginacaoViewModel.Id = item.Id;
                orcamentoPaginacaoViewModel.Categoria = item.Categoria.Nome;
                orcamentoPaginacaoViewModel.Receita = item.Tipo == "R" ? item.Valor : 0;
                orcamentoPaginacaoViewModel.Despesa = item.Tipo == "D" ? item.Valor : 0;

                totalReceitas = totalReceitas + orcamentoPaginacaoViewModel.Receita;
                totalDespesas = totalDespesas + orcamentoPaginacaoViewModel.Despesa;

                listaOrcamentoPaginacaoViewModel.Add(orcamentoPaginacaoViewModel);
            }

            saldoFinal = totalReceitas - totalDespesas;


            int recordCount = listaOrcamentoPaginacaoViewModel.Count();

            return Json(
                new
                {
                    iTotalRecords = recordCount,
                    iTotalDisplayRecords = recordCount,
                    aaData = listaOrcamentoPaginacaoViewModel,
                    iTotalReceitas = totalReceitas,
                    iTotalDespesas = totalDespesas,
                    iSaldoFinal = saldoFinal
                }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SalvarOrcamento(FormCollection formCadastro)
        {
            try
            {
                string retornoMensagem;
                string idOrcamento = formCadastro["idOrcamento"];
                string tipoOrcamento = formCadastro["tipoOrcamento"];
                string valorOrcamento = formCadastro["valorOrcamento"];
                string categoriaOrcamento = formCadastro["categoriaOrcamento"];
                DateTime dataOrcamento = DateTime.ParseExact("01/" + formCadastro["mesanoOrcamento"], "dd/MM/yyyy", null);

                OrcamentoDTO dto = new OrcamentoDTO();
                if (!String.IsNullOrEmpty(idOrcamento)) dto.Id = int.Parse(idOrcamento);
                dto.Tipo = tipoOrcamento;
                dto.Mes = dataOrcamento.Month;
                dto.Ano = dataOrcamento.Year;
                dto.Valor = Decimal.Parse(valorOrcamento, NumberStyles.Currency, new CultureInfo("pt-BR"));
                dto.CategoriaId = int.Parse(categoriaOrcamento);
                dto.UsuarioId = base.Usuario.Id;

                if (String.IsNullOrEmpty(idOrcamento))
                {
                    _orcamentoAppService.CadastrarOrcamento(dto);
                    retornoMensagem = "registro incluído com sucesso!";
                }
                else
                {
                    _orcamentoAppService.EditarOrcamento(dto);
                    retornoMensagem = "registro atualizado com sucesso!";
                }


                return Json(new { success = true, responseText = retornoMensagem }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }
          
        }

        public JsonResult ObterOrcamento(int id)
        {
            try
            {

                Orcamento orcamento = _orcamentoAppService.GetById(id);

                OrcamentoViewModel orcamentoViewModel = new OrcamentoViewModel();

                orcamentoViewModel.OrcamentoId = orcamento.Id;
                orcamentoViewModel.OrcamentoTipo = orcamento.Tipo;
                orcamentoViewModel.OrcamentoMes = orcamento.Mes;
                orcamentoViewModel.OrcamentoAno = orcamento.Ano;
                orcamentoViewModel.OrcamentoValor = orcamento.Valor;
                orcamentoViewModel.OrcamentoCategoriaId = orcamento.CategoriaId;

                return Json(orcamentoViewModel, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(new { success = false, responseText = ex.InnerException }, JsonRequestBehavior.AllowGet);
            }


        }


        public JsonResult ExcluirOrcamento(int id)
        {
            try
            {
                _orcamentoAppService.ExcluirOrcamento(id);

                return Json(new { success = true, responseText = "registro excluído com sucesso!" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(new { success = false, responseText = ex.Message.Replace("Validation failed:", "").Replace("--", "").Trim() }, JsonRequestBehavior.AllowGet);
            }


        }


    }
}