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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Financas.UI.WebMvc.Controllers
{
    [CustomAuthorize]
    public class LancamentoController : BaseController
    {
        private readonly ILancamentoAppService _lancamentoAppService;

        public LancamentoController(ILancamentoAppService lancamentoAppService)
            : base()
        {
            this._lancamentoAppService = lancamentoAppService;
        }

        public ActionResult Index()
        {
            return View(base.Usuario);
        }

        public ActionResult Processar(int ano, int mes)
        {
            decimal saldoAnterior = 0;
            decimal totalReceitas = 0;
            decimal totalDespesas = 0;
            decimal saldoMensal = 0;
            decimal saldoInicioMes = 0;
            decimal saldoFinalMes = 0;

            CultureInfo cultureInfo = new CultureInfo("pt-BR");

            DateTime dataInicial = DateTime.Parse(string.Format("1/{0}/{1}", mes, ano), cultureInfo);

            List<LancamentoMensalDTO> listaLancamentos = _lancamentoAppService.ObterLancamentosMensal(out saldoAnterior, base.Usuario.Id, mes, ano).OrderBy(o => o.Data).ToList();

            List<LancamentoPaginacaoViewModel> listaLancamentoPaginacaoViewModel = new List<LancamentoPaginacaoViewModel>();

            listaLancamentoPaginacaoViewModel.Add(new LancamentoPaginacaoViewModel { Data = dataInicial.ToString("dd/MM/yyyy", cultureInfo), Descricao = "Saldo Anterior", Saldo = saldoAnterior });

            saldoInicioMes = saldoAnterior;

            foreach (var item in listaLancamentos)
            {
                LancamentoPaginacaoViewModel lancamentoPaginacaoViewModel = new LancamentoPaginacaoViewModel();

                lancamentoPaginacaoViewModel.Id = item.Id;
                lancamentoPaginacaoViewModel.Data = item.Data.ToString("dd/MM/yyyy");
                lancamentoPaginacaoViewModel.Descricao = item.Descricao;
                lancamentoPaginacaoViewModel.Entrada = item.Entrada;
                lancamentoPaginacaoViewModel.Saida = item.Saida;

                saldoAnterior = saldoAnterior + (lancamentoPaginacaoViewModel.Entrada - lancamentoPaginacaoViewModel.Saida);
                lancamentoPaginacaoViewModel.Saldo = saldoAnterior;

                totalReceitas = totalReceitas + lancamentoPaginacaoViewModel.Entrada;
                totalDespesas = totalDespesas + lancamentoPaginacaoViewModel.Saida;

                listaLancamentoPaginacaoViewModel.Add(lancamentoPaginacaoViewModel);
            }

            saldoMensal = totalReceitas - totalDespesas;
            saldoFinalMes = saldoInicioMes + (totalReceitas - totalDespesas);

            int recordCount = listaLancamentoPaginacaoViewModel.Count();

            return Json(
                new
                {
                    iTotalRecords = recordCount,
                    iTotalDisplayRecords = recordCount,
                    aaData = listaLancamentoPaginacaoViewModel,
                    iTotalReceitas = totalReceitas,
                    iTotalDespesas = totalDespesas,
                    iSaldoMensal = saldoMensal,
                    iSaldoInicioMes = saldoInicioMes,
                    iSaldoFinalMes = saldoFinalMes

                }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SalvarLancamento(FormCollection formCadastro)
        {
            try
            {
                string retornoMensagem;
                string idLancamento = formCadastro["idLancamento"];
                string dataLancamento = formCadastro["dataLancamento"];
                string descricaoLancamento = formCadastro["descricaoLancamento"];
                string valorLancamento = formCadastro["valorLancamento"];
                string tipoLancamento = formCadastro["tipoLancamento"];
                string categoriaLancamento = formCadastro["categoriaLancamento"];


                LancamentoDTO dto = new LancamentoDTO();
                if (!String.IsNullOrEmpty(idLancamento)) dto.Id = int.Parse(idLancamento);
                dto.Data = DateTime.ParseExact(dataLancamento, "dd/MM/yyyy", null);
                dto.Descricao = descricaoLancamento;
                dto.Valor = Decimal.Parse(valorLancamento, NumberStyles.Currency, new CultureInfo("pt-BR"));
                dto.Tipo = tipoLancamento;
                dto.CategoriaId = int.Parse(categoriaLancamento);
                dto.UsuarioId = base.Usuario.Id;

                if (String.IsNullOrEmpty(idLancamento))
                {
                    _lancamentoAppService.CadastrarLancamento(dto);
                    retornoMensagem = "registro incluído com sucesso!";
                }
                else
                {
                    _lancamentoAppService.EditarLancamento(dto);
                    retornoMensagem = "registro atualizado com sucesso!";
                }


                return Json(new { success = true, responseText = retornoMensagem }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult ObterLancamento(int id)
        {
            try
            {

                Lancamento lancamento = _lancamentoAppService.GetById(id);

                LancamentoViewModel lancamentoViewModel = new LancamentoViewModel();

                lancamentoViewModel.LancamentoId = lancamento.Id;
                lancamentoViewModel.LancamentoData = lancamento.Data.ToString("dd/MM/yyyy");
                lancamentoViewModel.LancamentoDescricao = lancamento.Descricao;
                lancamentoViewModel.LancamentoCategoriaId = lancamento.CategoriaId;
                lancamentoViewModel.LancamentoTipo = lancamento.Tipo;
                lancamentoViewModel.LancamentoValor = lancamento.Valor;

                return Json(lancamentoViewModel, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(new { success = false, responseText = ex.InnerException }, JsonRequestBehavior.AllowGet);
            }


        }


        public JsonResult ExcluirLancamento(int id)
        {
            try
            {
                _lancamentoAppService.ExcluirLancamento(id);

                return Json(new { success = true, responseText = "registro excluído com sucesso!" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(new { success = false, responseText = ex.Message.Replace("Validation failed:", "").Replace("--", "").Trim() }, JsonRequestBehavior.AllowGet);
            }


        }


    }
}