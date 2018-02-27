using Financas.Domain.Common.Specifications;
using Financas.Domain.Contracts.Repositories;
using Financas.Domain.Entities;
using Financas.Domain.Specs.CategoriaSpecs;
using Financas.Infrastructure.Data.Contracts;
using Financas.Infrastructure.Data.Repositories.Common;

namespace Financas.Infrastructure.Data.Repositories
{
    public class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(IDbContext context)
            : base(context)
        {

        }

        public bool NomeNaoCadastrado(int? id, string nome, int? usuarioId, string tipo)
        {
            ISpecification<Categoria> criterio;

            criterio = new CategoriaPorUsuario(usuarioId.Value);
            criterio = criterio.And(new CategoriaPorTipo(tipo));
            criterio = criterio.And(new CategoriaPorNomeExato(nome));
            criterio = criterio.And(new CategoriaPorPorIdDiferente(id));

            return NotExists(criterio);

        }

        public bool UsuarioCadastroValido(int? id, int? usuarioId)
        {
            ISpecification<Categoria> criterio;

            criterio = new CategoriaPorUsuarioValido(id, usuarioId);

            return Exists(criterio);

        }


        public bool IdCategoriaExiste(int? id)
        {
            ISpecification<Categoria> criterio;

            criterio = new CategoriaPorIdExato(id);

            return Exists(criterio);

        }

        public bool CategoriasPaiNaoVinculadas(int? idCategoriaPai)
        {
            ISpecification<Categoria> criterio;

            criterio = new CategoriaPorIdPai(idCategoriaPai);

            return NotExists(criterio);

        }

    }
}
