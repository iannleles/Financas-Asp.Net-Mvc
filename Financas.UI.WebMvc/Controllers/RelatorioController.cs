
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
using CustomAuthetication.Helpers;

namespace Financas.UI.WebMvc.Controllers
{
    [CustomAuthorize]
    public class RelatorioController : BaseController
    {
        private readonly IRelatorioAppService _relatorioAppService;

        public RelatorioController(IRelatorioAppService relatorioAppService)
            : base()
        {
          this._relatorioAppService = relatorioAppService;
        }

        public ActionResult PrevistoRealizado()
        {
            return View(base.Usuario);
        }

        public ActionResult LancamentosMeses()
        {
            return View(base.Usuario);
        }

        public ActionResult LancamentosPeriodo()
        {
            return View(base.Usuario);
        }

        public ActionResult ProcessarPrevistoRealizado(int ano, int mes)
        {
            decimal totalReceitasPrevisto = 0;
            decimal totalReceitasRealizado = 0;
            decimal totalReceitaDiferenca = 0;
            decimal totalDespesasPrevisto = 0;
            decimal totalDespesasRealizado = 0;
            decimal totalDespesasDiferenca = 0;
            decimal saldoMensalPrevisto = 0;
            decimal saldoMensalRealizado = 0;
            decimal saldoMensalDiferenca = 0;
            decimal saldoInicioMesRealizado = 0;
            decimal saldoFinalMesRealizado = 0;

            List<RelatorioPrevistoRealizadoDTO> dto = _relatorioAppService.ObterRelatorioPrevistoRealizado(out saldoInicioMesRealizado, base.Usuario.Id, ano, mes);
            List<RelatorioOrcamentoViewModel> listaOrcamentoViewModel = new List<RelatorioOrcamentoViewModel>();

            var Receitas = dto.Where(x => x.CategoriaTipoPai == "R").ToList();

            foreach (var itemFilho in Receitas.OrderBy(x => x.CategoriaNomeFilho))
            {

                RelatorioOrcamentoViewModel orcamento = new RelatorioOrcamentoViewModel();
                orcamento.CategoriaPaiId = 0;
                orcamento.CategoriaPaiNome = "Receitas";
                orcamento.CategoriaPaiTipo = "R";
                orcamento.CategoriaNome = itemFilho.CategoriaNomeFilho;
                orcamento.Previsto = itemFilho.Previsto;
                orcamento.Realizado = itemFilho.Realizado;
                orcamento.Diferenca = itemFilho.Diferenca;

                totalReceitasPrevisto = totalReceitasPrevisto + itemFilho.Previsto;
                totalReceitasRealizado = totalReceitasRealizado + itemFilho.Realizado;

                listaOrcamentoViewModel.Add(orcamento);

            }

            var Despesas = dto.Where(x => x.CategoriaTipoPai == "D").ToList();

            foreach (var itemFilho in Despesas.OrderBy(x => x.CategoriaNomeFilho))
            {

                RelatorioOrcamentoViewModel orcamento = new RelatorioOrcamentoViewModel();
                orcamento.CategoriaPaiId = itemFilho.CategoriaIdPai;
                orcamento.CategoriaPaiTipo = itemFilho.CategoriaTipoPai;
                orcamento.CategoriaPaiNome = itemFilho.CategoriaNomePai;
                orcamento.CategoriaNome = itemFilho.CategoriaNomeFilho;
                orcamento.Previsto = itemFilho.Previsto;
                orcamento.Realizado = itemFilho.Realizado;
                orcamento.Diferenca = itemFilho.Diferenca;

                totalDespesasPrevisto = totalDespesasPrevisto + itemFilho.Previsto;
                totalDespesasRealizado = totalDespesasRealizado + itemFilho.Realizado;

                listaOrcamentoViewModel.Add(orcamento);

            }



            totalReceitaDiferenca = totalReceitasPrevisto - totalReceitasRealizado;
            totalDespesasDiferenca = totalDespesasPrevisto - totalDespesasRealizado;
            saldoMensalDiferenca = saldoMensalPrevisto - saldoMensalRealizado;

            saldoMensalPrevisto = totalReceitasPrevisto - totalDespesasPrevisto;
            saldoMensalRealizado = totalReceitasRealizado - totalDespesasRealizado;
            saldoFinalMesRealizado = saldoInicioMesRealizado + (totalReceitasRealizado - totalDespesasRealizado);

            int recordCount = listaOrcamentoViewModel.Count();

            var dados = listaOrcamentoViewModel.OrderBy(x => x.CategoriaPaiNome).ToList();

            return Json(
            new
            {
                iTotalRecords = recordCount,
                iTotalDisplayRecords = recordCount,
                aaData = dados,
                iTotalReceitasPrevisto = totalReceitasPrevisto,
                iTotalReceitasRealizado = totalReceitasRealizado,
                iTotalReceitasDiferenca = totalReceitaDiferenca,
                iTotalDespesasPrevisto = totalDespesasPrevisto,
                iTotalDespesasRealizado = totalDespesasRealizado,
                iTotalDespesasDiferenca = totalDespesasDiferenca,
                iSaldoMensalPrevisto = saldoMensalPrevisto,
                iSaldoMensalRealizado = saldoMensalRealizado,
                iSaldoMensalDiferenca = saldoMensalDiferenca,
                iSaldoInicioMesRealizado = saldoInicioMesRealizado,
                iSaldoFinalMesRealizado = saldoFinalMesRealizado
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProcessarLancamentosMeses(string ano)
        {
            decimal totalReceitas = 0;
            decimal totalDespesas = 0;
            decimal saldoFinal = 0;

            RelatorioLancamentosPorMesesDTO dto = _relatorioAppService.ObterRelatorioLancamentosPorMeses(base.Usuario.Id, int.Parse(ano));
            List<RelatorioLancamentoViewModel> listaLancamentoViewModel = new List<RelatorioLancamentoViewModel>();

            foreach (var itemFilho in dto.RelatorioLancamentosPorMesesPaiDTO.OrderBy(x => x.CategoriaIdPai).ThenBy(o => o.CategoriaNomeFilho))
            {

                RelatorioLancamentoViewModel lancamento = new RelatorioLancamentoViewModel();
                lancamento.CategoriaPaiId = itemFilho.CategoriaIdPai;
                lancamento.CategoriaPaiNome = itemFilho.CategoriaNomePai;
                lancamento.CategoriaPaiTipo = itemFilho.CategoriaTipoPai;
                lancamento.CategoriaNome = itemFilho.CategoriaNomeFilho;

                lancamento.Jan = dto.RelatorioLancamentosPorMesesFilhoDTO.Where(x => x.CategoriaIdFilho == itemFilho.CategoriaIdFilho && x.Mes == 1).Sum(x => x.Total);
                lancamento.Fev = dto.RelatorioLancamentosPorMesesFilhoDTO.Where(x => x.CategoriaIdFilho == itemFilho.CategoriaIdFilho && x.Mes == 2).Sum(x => x.Total);
                lancamento.Mar = dto.RelatorioLancamentosPorMesesFilhoDTO.Where(x => x.CategoriaIdFilho == itemFilho.CategoriaIdFilho && x.Mes == 3).Sum(x => x.Total);
                lancamento.Abr = dto.RelatorioLancamentosPorMesesFilhoDTO.Where(x => x.CategoriaIdFilho == itemFilho.CategoriaIdFilho && x.Mes == 4).Sum(x => x.Total);
                lancamento.Mai = dto.RelatorioLancamentosPorMesesFilhoDTO.Where(x => x.CategoriaIdFilho == itemFilho.CategoriaIdFilho && x.Mes == 5).Sum(x => x.Total);
                lancamento.Jun = dto.RelatorioLancamentosPorMesesFilhoDTO.Where(x => x.CategoriaIdFilho == itemFilho.CategoriaIdFilho && x.Mes == 6).Sum(x => x.Total);
                lancamento.Jul = dto.RelatorioLancamentosPorMesesFilhoDTO.Where(x => x.CategoriaIdFilho == itemFilho.CategoriaIdFilho && x.Mes == 7).Sum(x => x.Total);
                lancamento.Ago = dto.RelatorioLancamentosPorMesesFilhoDTO.Where(x => x.CategoriaIdFilho == itemFilho.CategoriaIdFilho && x.Mes == 8).Sum(x => x.Total);
                lancamento.Set = dto.RelatorioLancamentosPorMesesFilhoDTO.Where(x => x.CategoriaIdFilho == itemFilho.CategoriaIdFilho && x.Mes == 9).Sum(x => x.Total);
                lancamento.Out = dto.RelatorioLancamentosPorMesesFilhoDTO.Where(x => x.CategoriaIdFilho == itemFilho.CategoriaIdFilho && x.Mes == 10).Sum(x => x.Total);
                lancamento.Nov = dto.RelatorioLancamentosPorMesesFilhoDTO.Where(x => x.CategoriaIdFilho == itemFilho.CategoriaIdFilho && x.Mes == 11).Sum(x => x.Total);
                lancamento.Dez = dto.RelatorioLancamentosPorMesesFilhoDTO.Where(x => x.CategoriaIdFilho == itemFilho.CategoriaIdFilho && x.Mes == 12).Sum(x => x.Total);

                lancamento.Total = dto.RelatorioLancamentosPorMesesFilhoDTO.Where(x => x.CategoriaIdFilho == itemFilho.CategoriaIdFilho).Sum(x => x.Total);

                if(itemFilho.CategoriaTipoPai == "R"){
                     totalReceitas = totalReceitas + lancamento.Total;
                } 
                else 
                {
                    totalDespesas = totalDespesas + lancamento.Total;
                }

                listaLancamentoViewModel.Add(lancamento);

            }


            saldoFinal = totalReceitas - totalDespesas;

            int recordCount = listaLancamentoViewModel.Count();

            var dados = listaLancamentoViewModel.OrderBy(x => x.CategoriaPaiNome).ToList();

            return Json(
            new
            {
                iTotalRecords = recordCount,
                iTotalDisplayRecords = recordCount,
                aaData = dados,
                iTotalReceitas = totalReceitas,
                iTotalDespesas = totalDespesas,
                iSaldoFinal = saldoFinal
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProcessarLancamentosPeriodo(string _dataInicial, string _dataFinal)
        {
            decimal saldoAnterior = 0;
            decimal totalReceitas = 0;
            decimal totalDespesas = 0;
            decimal saldoMensal = 0;
            decimal saldoInicioMes = 0;
            decimal saldoFinalMes = 0;

            CultureInfo cultureInfo = new CultureInfo("pt-BR");

            DateTime dataInicial = DateTime.Parse(_dataInicial, cultureInfo);
            
            DateTime dataFinal = DateTime.Parse(_dataFinal, cultureInfo);

            List<RelatorioLancamentosPorPeriodoDTO> listaLancamentos = _relatorioAppService.ObterRelatorioLancamentosPorPeriodo(out saldoAnterior, base.Usuario.Id, dataInicial, dataFinal).ToList();

            List<LancamentoPaginacaoViewModel> listaLancamentoPaginacaoViewModel = new List<LancamentoPaginacaoViewModel>();

            listaLancamentoPaginacaoViewModel.Add(new LancamentoPaginacaoViewModel { Data = dataInicial.ToString("dd/MM/yyyy", cultureInfo), Descricao = "Saldo Anterior", Saldo = saldoAnterior });

            saldoInicioMes = saldoAnterior;

            foreach (var item in listaLancamentos)
            {
                LancamentoPaginacaoViewModel lancamentoPaginacaoViewModel = new LancamentoPaginacaoViewModel();

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



    }
}