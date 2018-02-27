using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Financas.Domain.Common.Specifications;
using Financas.Domain.Contracts.Repositories.Common;
using Financas.Domain.ValueObjects;
using Financas.Infrastructure.Data.Contracts;
using Financas.Infrastructure.Data.Extensions;

namespace Financas.Infrastructure.Data.Repositories.Common
{

    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
       where TEntity : class
    {
        protected readonly IDbContext dbContext;
        private readonly DbSet<TEntity> entitySet;

        public GenericRepository(IDbContext context)
        {
            dbContext = context;
            entitySet = dbContext.Set<TEntity>();
        }

        protected DbSet<TEntity> EntitySet
        {
            get { return entitySet; }
        }

        /// <summary>
        /// Adicionar uma Entidade no Repositório
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(
            TEntity entity)
        {
            entitySet.Add(entity);
        }

        /// <summary>
        /// Atualizar uma Entidade no Repositório
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(
            TEntity entity)
        {
            //Encontrar 'entrada' da Entidade no Contexto
            var entry = dbContext.Entry(entity);

            //Obter valores do(s) atributo(s) identificadores da Entidade
            IEnumerable<object> keys = dbContext.GetPropertyKeyValues<TEntity>(entity);

            if (entry.State == EntityState.Detached)
            {
                //Obter valores atuais da Entidade
                var currentEntry = this.entitySet.Find(keys.ToArray());
                if (currentEntry != null)
                {
                    var attachedEntry = this.dbContext.Entry(currentEntry);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    this.entitySet.Attach(entity);
                    entry.State = EntityState.Modified;
                }
            }
        }

        /// <summary>
        /// Remover uma Entidade do Repositório a partir do(s) valor(es) chave(s)
        /// </summary>
        /// <param name="keys"></param>
        public virtual void Delete(
            params object[] keys)
        {
            TEntity entity = GetById(keys);

            if (entity != null)
            {
                entitySet.Remove(entity);
            }
        }

        /// <summary>
        /// Remover uma Entidade do Repositório a partir da própria Entidade
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(
            TEntity entity)
        {
            entitySet.Remove(entity);
        }

        /// <summary>
        /// Remover Entidade(s) do Repositório que atendam ao critério estabelecido
        /// </summary>
        /// <param name="predicate"></param>
        public virtual void Delete(
            ISpecification<TEntity> predicate)
        {
            foreach (TEntity entity in Get(predicate))
            {
                entitySet.Remove(entity);
            }
        }

        /// <summary>
        /// Obter uma Entidade do Repositório a partir do(s) valor(es) chave(s)
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public TEntity GetById(
            params object[] keys)
        {
            return entitySet.Find(keys);
        }

        /// <summary>
        /// Obter a primeira Entidade do Repositório
        /// </summary>
        /// <returns></returns>
        public TEntity First()
        {
            return entitySet.FirstOrDefault();
        }

        /// <summary>
        /// Obter a última Entidade do Repositório que atenda ao critério estabelecido
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TEntity First(
            ISpecification<TEntity> predicate)
        {
            return entitySet
                    .Where(predicate.SpecExpression)
                    .FirstOrDefault();
        }

        /// <summary>
        /// Obter a primeira Entidade do Repositório
        /// </summary>
        /// <returns></returns>
        public TEntity Last()
        {
            return entitySet.LastOrDefault();
        }

        /// <summary>
        /// Obter a última Entidade do Repositório que atenda ao critério estabelecido
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TEntity Last(
            ISpecification<TEntity> predicate)
        {
            return entitySet
                    .Where(predicate.SpecExpression)
                    .LastOrDefault();
        }

        /// <summary>
        /// Obter todas as Entidades do Repositório
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get()
        {
            return entitySet.AsEnumerable();
        }

        /// <summary>
        /// Obter todas as Entidades do Repositório sob determina(s) ordem(ns)
        /// </summary>
        /// <param name="sorts"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get(
            SortField[] sorts)
        {
            return entitySet
                    .ApplyMultipleOrders(sorts);
        }

        /// <summary>
        /// Obter todas as Entidades do Repositório que atendam ao critério especificado
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get(
            ISpecification<TEntity> predicate)
        {
            return entitySet
                    .Where(predicate.SpecExpression);
        }

        /// <summary>
        /// Obter todas as Entidades do Repositório que atendam ao critério especificado, sob determina(s) ordem(ns)
        /// </summary>
        /// <param name="sorts"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get(
            SortField[] sorts,
            ISpecification<TEntity> predicate)
        {
            return entitySet
                    .Where(predicate.SpecExpression)
                    .ApplyMultipleOrders(sorts);
        }

        /// <summary>
        /// Obter todas as Entidades do Repositório sob determina(s) ordem(ns), com limitação de paginação
        /// </summary>
        /// <param name="recordCount"></param>
        /// <param name="sorts"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get(
            out int recordCount,
            SortField[] sorts,
            int pageSize = 10,
            int page = 1)
        {
            recordCount = entitySet.Count();

            return entitySet
                    .ApplyMultipleOrders(sorts)
                    .Skip(((page - 1) * (int)pageSize))
                    .Take((int)pageSize);
        }

        /// <summary>
        /// Obter todas as Entidades do Repositório que atendam ao critério especificado, sob determina(s) ordem(ns), com limitação de paginação
        /// </summary>
        /// <param name="recordCount"></param>
        /// <param name="sorts"></param>
        /// <param name="predicate"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get(
            out int recordCount,
            SortField[] sorts,
            ISpecification<TEntity> predicate,
            int pageSize = 10,
            int page = 1)
        {
            recordCount = entitySet.Count(predicate.SpecExpression);

            return entitySet
                    .Where(predicate.SpecExpression)
                    .ApplyMultipleOrders(sorts)
                    .Skip(((page - 1) * (int)pageSize))
                    .Take((int)pageSize);
        }

        /// <summary>
        /// Obter quantidade total de Entidades do Repositório
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            return entitySet.Count();
        }

        /// <summary>
        /// Obter quantidade de Entidades do Repositório que atendam ao critério especificado
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual int Count(
            ISpecification<TEntity> predicate)
        {
            return entitySet.Count(predicate.SpecExpression);
        }

        /// <summary>
        /// Verificar se existe ou não Entidades que atendam ao critério especificado
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual bool Exists(
            ISpecification<TEntity> predicate)
        {
            return entitySet.Count(predicate.SpecExpression) > 0;
        }
        public virtual bool NotExists(
            ISpecification<TEntity> predicate)
        {
            return entitySet.Count(predicate.SpecExpression) == 0;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
