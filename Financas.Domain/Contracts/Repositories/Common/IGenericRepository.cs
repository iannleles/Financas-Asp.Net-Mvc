using System;
using System.Collections.Generic;
using Financas.Domain.Common.Specifications;
using Financas.Domain.ValueObjects;

namespace Financas.Domain.Contracts.Repositories.Common
{
    public interface IGenericRepository<TEntity> : IDisposable
             where TEntity : class
    {

        /// <summary>
        /// Adicionar uma Entidade no Repositório
        /// </summary>
        /// <param name="entity"></param>
        void Add(
            TEntity entity);

        /// <summary>
        /// Atualizar uma Entidade no Repositório
        /// </summary>
        /// <param name="entity"></param>
        void Update(
            TEntity entity);

        /// <summary>
        /// Remover uma Entidade do Repositório a partir do(s) valor(es) chave(s)
        /// </summary>
        /// <param name="keys"></param>
        void Delete(
            params object[] keys);

        /// <summary>
        /// Remover uma Entidade do Repositório a partir da própria Entidade
        /// </summary>
        /// <param name="entity"></param>
        void Delete(
            TEntity entity);

        /// <summary>
        /// Remover Entidade(s) do Repositório que atendam ao critério estabelecido
        /// </summary>
        /// <param name="predicate"></param>
        void Delete(
            ISpecification<TEntity> predicate);

        /// <summary>
        /// Obter uma Entidade do Repositório a partir do(s) valor(es) chave(s)
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        TEntity GetById(
            params object[] keys);

        /// <summary>
        /// Obter a primeira Entidade do Repositório
        /// </summary>
        /// <returns></returns>
        TEntity First();

        /// <summary>
        /// Obter a última Entidade do Repositório que atenda ao critério estabelecido
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity First(
            ISpecification<TEntity> predicate);

        /// <summary>
        /// Obter a primeira Entidade do Repositório
        /// </summary>
        /// <returns></returns>
        TEntity Last();

        /// <summary>
        /// Obter a última Entidade do Repositório que atenda ao critério estabelecido
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity Last(
            ISpecification<TEntity> predicate);

        /// <summary>
        /// Obter todas as Entidades do Repositório
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> Get();

        /// <summary>
        /// Obter todas as Entidades do Repositório sob determina(s) ordem(ns)
        /// </summary>
        /// <param name="sorts"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(
            SortField[] sorts);

        /// <summary>
        /// Obter todas as Entidades do Repositório que atendam ao critério especificado
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(
            ISpecification<TEntity> predicate);

        /// <summary>
        /// Obter todas as Entidades do Repositório que atendam ao critério especificado, sob determina(s) ordem(ns)
        /// </summary>
        /// <param name="sorts"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(
            SortField[] sorts,
            ISpecification<TEntity> predicate);

        /// <summary>
        /// Obter todas as Entidades do Repositório sob determina(s) ordem(ns), com limitação de paginação
        /// </summary>
        /// <param name="recordCount"></param>
        /// <param name="sorts"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(
            out int recordCount,
            SortField[] sorts,
            int pageSize = 10,
            int page = 1);

        /// <summary>
        /// Obter todas as Entidades do Repositório que atendam ao critério especificado, sob determina(s) ordem(ns), com limitação de paginação
        /// </summary>
        /// <param name="recordCount"></param>
        /// <param name="sorts"></param>
        /// <param name="predicate"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        IEnumerable<TEntity> Get(
            out int recordCount,
            SortField[] sorts,
            ISpecification<TEntity> predicate,
            int pageSize = 10,
            int page = 1);

        /// <summary>
        /// Obter quantidade total de Entidades do Repositório
        /// </summary>
        /// <returns></returns>
        int Count();

        /// <summary>
        /// Obter quantidade de Entidades do Repositório que atendam ao critério especificado
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        int Count(
            ISpecification<TEntity> predicate);

        /// <summary>
        /// Verificar se existe ou não Entidades que atendam ao critério especificado
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        bool Exists(
            ISpecification<TEntity> predicate);
        bool NotExists(
            ISpecification<TEntity> predicate);

    }
}
