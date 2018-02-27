using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Infrastructure.Data.Contracts
{
    /// <summary>
    /// Define a manipulação de contratos e acesso a conjuntos de entidades, bem como operações de gestão transacional.
    /// </summary>
    public interface IDbContext : IDisposable
    {
        /// <summary>
        /// Fornece acesso ao conjunto de entidade genérica definido para a manipulação no contexto.
        /// </summary>
        /// <typeparam name="TEntity">Entidade genérica que será mapeada no contexto.</typeparam>
        /// <returns></returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        /// Fornece acesso ao ponto de entrada da instância de entidade definida como um argumento.
        /// </summary>
        /// <typeparam name="TEntity">Entidade genérica que será mapeada no contexto.</typeparam>
        /// <param name="entity">Instância da entidade genérica.</param>
        /// <returns></returns>
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Efetivação das mudanças feitas no contexto.
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        IEnumerable<string> GetPropertyKeys<TEntity>(TEntity entity) where TEntity : class;

        List<object> GetPropertyKeyValues<TEntity>(TEntity entity) where TEntity : class;

        DbRawSqlQuery<T> SQLQuery<T>(string sql, params object[] parameters);

        decimal SqlQuerySingleDecimal(string query);


    }
}
