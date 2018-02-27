
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Financas.UI.WebMvc.Helpers;
using Financas.Domain.Dtos;
using Financas.Domain.Entities;
using Financas.Domain.Contracts.Services;
using Financas.UI.WebMvc.ViewModels;

namespace Financas.UI.WebMvc.Helpers
{
    public class GerenciaSessionCategoria : GerenciaSessionBase
    {
        private readonly ICategoriaAppService _categoriaAppService;
        private const string nomeSessionCategoriaReceita = "CategoriasReceita";
        private const string nomeSessionCategoriaDespesa = "CategoriasDespesa";

        public GerenciaSessionCategoria(ICategoriaAppService categoriaAppService)
        {
            this._categoriaAppService = categoriaAppService;
        }

        public void LoadCategoriaSession()
        {
            int? idUsuario = LeComDefault<UsuarioDTO>("Usuario").Id;

            List<Categoria> listaCategorias = _categoriaAppService.ListarCategoriasPorUsuario(idUsuario).ToList();

            List<Categoria> listaCategoriasReceitaPai = listaCategorias.Where(c => c.Tipo == "R" && c.CategoriaPaiId == null).OrderBy(o => o.Nome).ToList();

            List<CategoriaAgrupadasViewModel> listaCategoriasReceitaAgrupadasViewModel = new List<CategoriaAgrupadasViewModel>();

            foreach (var item in listaCategoriasReceitaPai)
            {
                List<CategoriaViewModel> listaCategoriaFilha = new List<CategoriaViewModel>();

                if (item.CategoriaFilhas != null)
                {
                    foreach (var filha in item.CategoriaFilhas.OrderBy(o => o.Nome))
                    {
                        listaCategoriaFilha.Add(new CategoriaViewModel { CategoriaId = filha.Id, CategoriaNome = filha.Nome });
                    }
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

                if (item.CategoriaFilhas != null)
                {
                    foreach (var filha in item.CategoriaFilhas.OrderBy(o => o.Nome))
                    {
                        listaCategoriaFilha.Add(new CategoriaViewModel { CategoriaId = filha.Id, CategoriaNome = filha.Nome });
                    }
                }

                listaCategoriaDespesaAgrupadasViewModel.Add(new CategoriaAgrupadasViewModel
                {
                    CategoriaPaiId = item.Id,
                    CategoriaPaiNome = item.Nome,
                    CategoriaFilha = listaCategoriaFilha
                });
            }

            GerenciaSessionBase.Atualiza("CategoriasReceita", listaCategoriasReceitaAgrupadasViewModel);
            GerenciaSessionBase.Atualiza("CategoriasDespesa", listaCategoriaDespesaAgrupadasViewModel);
        }

    }
}