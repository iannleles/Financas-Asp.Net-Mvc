using Financas.Domain.Common.Specifications;
using Financas.Domain.Contracts.Repositories;
using Financas.Domain.Dtos;
using Financas.Domain.Entities;
using Financas.Domain.Specs.LancamentoSpecs;
using Financas.Infrastructure.Data.Contracts;
using Financas.Infrastructure.Data.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace Financas.Infrastructure.Data.Repositories
{
    public class LancamentoRepository : GenericRepository<Lancamento>, ILancamentoRepository
    {
        public LancamentoRepository(IDbContext context)
            : base(context)
        {

        }

        public bool CategoriasNaoVinculadas(int? categoriaId)
        {
            ISpecification<Lancamento> criterio;

            criterio = new LancamentoPorIdCategoria(categoriaId);

            return NotExists(criterio);

        }

        public decimal ObterSaldoInicial(int? usuarioId, int dia, int mes, int ano)
        {

            string sqlQuery = string.Format("SELECT dbo.func_SaldoInicial({0},{1},{2},{3})", usuarioId, dia, mes, ano);
            return this.dbContext.SqlQuerySingleDecimal(sqlQuery);

        }


        public List<LancamentoMensalDTO> ObterLancamentosMensal(int? usuarioId, int mes, int ano)
        {
            try
            {

                string sqlQuery = string.Format("EXEC sp_GetLancamentosMensal {0},{1},{2}", usuarioId, mes, ano);

                IEnumerable<LancamentoMensalDTO> data = this.dbContext.SQLQuery<LancamentoMensalDTO>(sqlQuery);


                return data.ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<RelatorioLancamentosPorPeriodoDTO> ObterRelatorioLancamentosPorPeriodo(int? usuarioId, DateTime dataInicial, DateTime dataFinal)
        {
            try
            {

                string sqlQuery = string.Format("EXEC sp_GetRelatorioLancamentosPeriodo {0},'{1}','{2}'", usuarioId, dataInicial, dataFinal);

                IEnumerable<RelatorioLancamentosPorPeriodoDTO> data = this.dbContext.SQLQuery<RelatorioLancamentosPorPeriodoDTO>(sqlQuery);


                return data.ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public RelatorioLancamentosPorMesesDTO ObterRelatorioLancamentosPorMeses(int? usuarioId, int ano)
        {
            try
            {
                RelatorioLancamentosPorMesesDTO dto = new RelatorioLancamentosPorMesesDTO();

                string sqlQueryPai = string.Format("EXEC sp_GetRelatorioLancamentosMesesPai {0},{1}", usuarioId, ano);
                string sqlQueryFilho = string.Format("EXEC sp_GetRelatorioLancamentosMesesFilho {0},{1}", usuarioId, ano);

                IEnumerable<RelatorioLancamentosPorMesesPaiDTO> dataPai = this.dbContext.SQLQuery<RelatorioLancamentosPorMesesPaiDTO>(sqlQueryPai);
                IEnumerable<RelatorioLancamentosPorMesesFilhoDTO> dataFilho = this.dbContext.SQLQuery<RelatorioLancamentosPorMesesFilhoDTO>(sqlQueryFilho);

                dto.RelatorioLancamentosPorMesesPaiDTO = dataPai.ToList();
                dto.RelatorioLancamentosPorMesesFilhoDTO = dataFilho.ToList();

                return dto;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        
        public List<RelatorioPrevistoRealizadoDTO> ObterRelatorioPrevistoRealizado(int? usuarioId, int ano, int mes)
        {
            try
            {

                string sqlQuery = string.Format("EXEC sp_GetRelatorioPrevistoRealizado {0},{1},{2}", usuarioId, mes, ano);

                IEnumerable<RelatorioPrevistoRealizadoDTO> data = this.dbContext.SQLQuery<RelatorioPrevistoRealizadoDTO>(sqlQuery);

                return data.ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DashboardHomeDTO ObterDashboardHome(int? usuarioId, int ano, int mes)
        {
            try
            {
                DashboardHomeDTO dto = new DashboardHomeDTO();

                string sqlQueryTotalizacao = string.Format("EXEC sp_GetDashboardHomeTotalizacao {0},{1},{2}", usuarioId, mes, ano);
                string sqlQueryDespesasPai = string.Format("EXEC sp_GetDashboardDiarioDespesasPai {0},{1},{2}", usuarioId, mes, ano);
                string sqlQueryDespesasFilha = string.Format("EXEC sp_GetDashboardDiarioDespesasFilho {0},{1},{2}", usuarioId, mes, ano);
                string sqlQueryMeses = string.Format("EXEC [sp_GetDashboardMeses] {0},{1}", usuarioId, ano);

                IEnumerable<DashboardHomeTotalizacaoDTO> dataTotalizacao = this.dbContext.SQLQuery<DashboardHomeTotalizacaoDTO>(sqlQueryTotalizacao);
                IEnumerable<DashboardHomeDespesasPaiDTO> dataDespesasPai = this.dbContext.SQLQuery<DashboardHomeDespesasPaiDTO>(sqlQueryDespesasPai);
                IEnumerable<DashboardHomeDespesasFilhoDTO> dataDespesasFilho = this.dbContext.SQLQuery<DashboardHomeDespesasFilhoDTO>(sqlQueryDespesasFilha);
                IEnumerable<DashboardHomeMesesDTO> dataMeses = this.dbContext.SQLQuery<DashboardHomeMesesDTO>(sqlQueryMeses);

                dto.DashboardHomeTotalizacaoDTO = dataTotalizacao.FirstOrDefault();
                dto.DashboardHomeDespesasPaiDTO = dataDespesasPai.ToList();
                dto.DashboardHomeDespesasFilhoDTO = dataDespesasFilho.ToList();
                dto.DashboardHomeMeses = dataMeses.ToList();

                return dto;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DashboardPrevistoRealizadoDTO ObterDashboardPrevistoRealizado(int? usuarioId, int ano, int mes)
        {
            try
            {
                DashboardPrevistoRealizadoDTO dto = new DashboardPrevistoRealizadoDTO();
                string sqlQueryPrevistoRealizado = string.Format("EXEC sp_GetDashboardHomePrevistoRealizado {0},{1},{2}", usuarioId, mes, ano);
                string sqlQueryMeses = string.Format("EXEC [sp_GetDashboardMeses] {0},{1}", usuarioId, ano);

                IEnumerable<DashboardHomePrevistoRealizadoCategoriaDTO> dataPrevistoRealizado = this.dbContext.SQLQuery<DashboardHomePrevistoRealizadoCategoriaDTO>(sqlQueryPrevistoRealizado);
                IEnumerable<DashboardPrevistoRealizadoMesesDTO> dataMeses = this.dbContext.SQLQuery<DashboardPrevistoRealizadoMesesDTO>(sqlQueryMeses);

                dto.Categorias = dataPrevistoRealizado.ToList();
                dto.Meses = dataMeses.ToList();

                return dto;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DashboardDiarioDTO ObterDashboardDiario(int? usuarioId, int ano, int mes)
        {
            try
            {
                DashboardDiarioDTO dto = new DashboardDiarioDTO();

                string sqlQueryTotalizacao = string.Format("EXEC sp_GetDashboardHomeTotalizacao {0},{1},{2}", usuarioId, mes, ano);
                string sqlQueryFluxoCaixa = string.Format("EXEC sp_GetDashboardDiarioFluxoCaixa {0},{1},{2}", usuarioId, mes, ano);
                string sqlQueryDespesasPai = string.Format("EXEC sp_GetDashboardDiarioDespesasPai {0},{1},{2}", usuarioId, mes, ano);
                string sqlQueryDespesasFilha = string.Format("EXEC sp_GetDashboardDiarioDespesasFilho {0},{1},{2}", usuarioId, mes, ano);


                IEnumerable<DashboardDiarioTotalizacaoDTO> dataTotalizacao = this.dbContext.SQLQuery<DashboardDiarioTotalizacaoDTO>(sqlQueryTotalizacao);
                IEnumerable<decimal> dataFluxoCaixa = this.dbContext.SQLQuery<decimal>(sqlQueryFluxoCaixa);
                IEnumerable<DashboardDiarioDespesasPaiDTO> dataDespesasPai = this.dbContext.SQLQuery<DashboardDiarioDespesasPaiDTO>(sqlQueryDespesasPai);
                IEnumerable<DashboardDiarioDespesasFilhoDTO> dataDespesasFilho = this.dbContext.SQLQuery<DashboardDiarioDespesasFilhoDTO>(sqlQueryDespesasFilha);

                dto.DashboardDiarioTotalizacaoDTO = dataTotalizacao.FirstOrDefault();
                dto.DashboardDiarioFluxoCaixaDTO = dataFluxoCaixa.ToList();
                dto.DashboardDiarioDespesasPaiDTO = dataDespesasPai.ToList();
                dto.DashboardDiarioDespesasFilhoDTO = dataDespesasFilho.ToList();

                return dto;
            }
                       catch (Exception ex)
            {
                
                throw ex;
            }

        }

        public List<DashboardMesesDTO> ObterDashboardMeses(int? usuarioId, int ano)
        {
            try
            {
                string sqlQuery = string.Format("EXEC sp_GetDashboardMeses {0},{1}", usuarioId, ano);

                IEnumerable<DashboardMesesDTO> data = this.dbContext.SQLQuery<DashboardMesesDTO>(sqlQuery);

                return data.ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<DashboardAnosDTO> ObterDashboardAnos(int? usuarioId, int anoInicial, int anoFinal)
        {
            try
            {
                string sqlQuery = string.Format("EXEC sp_GetDashboardAnos {0},{1},{2}", usuarioId, anoInicial, anoFinal);

                IEnumerable<DashboardAnosDTO> data = this.dbContext.SQLQuery<DashboardAnosDTO>(sqlQuery);

                return data.ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}

