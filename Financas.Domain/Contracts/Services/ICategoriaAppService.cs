
using System;
using System.Linq;
using Financas.Domain.Dtos;
using Financas.Domain.Entities;
using Financas.Domain.ValueObjects;

namespace Financas.Domain.Contracts.Services
{
    public interface ICategoriaAppService : IDisposable
    {
        void CadastrarCategoria(CategoriaDTO categoriaDTO);

        void EditarCategoria(CategoriaDTO categoriaDTO);

        void ExcluirCategoria(int categoriaId);

        Categoria GetById(int Id);

        IQueryable<Categoria> ListarCategoriaPaginacao(out int recordCount, string tipo, string nome, int? usuarioId, SortField[] sorts, int pageSize, int page);

        IQueryable<Categoria> ListarCategoriasPorTipo(string tipo, int? usuarioId);

        IQueryable<Categoria> ListarCategoriasPorUsuario(int? usuarioId);

    }
}
