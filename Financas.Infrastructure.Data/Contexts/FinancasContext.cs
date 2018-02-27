using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Financas.Domain.Entities;
using Financas.Infrastructure.Data.Contracts;
using Financas.Infrastructure.Data.Mappings;
using System;


namespace Financas.Infrastructure.Data.Contexts
{
    public class FinancasContext : DbContext, IDbContext
    {

        static FinancasContext()
        {
            Database.SetInitializer<FinancasContext>(null);
        }

        public FinancasContext()
            : base("dbFinancas")
        {
        }

        #region DbSets

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<Lancamento> Lancamentos { get; set; }
        public DbSet<Orcamento> Orcamentos { get; set; }

        #endregion



        public IEnumerable<string> GetPropertyKeys<TEntity>(TEntity entity)
         where TEntity : class
        {
            ObjectContext objectContext = ((IObjectContextAdapter)this).ObjectContext;

            ObjectSet<TEntity> set = objectContext.CreateObjectSet<TEntity>();

            IEnumerable<string> keyNames = set.EntitySet.ElementType
                                            .KeyMembers
                                            .Select(k => k.Name);

            return keyNames;
        }

        public List<object> GetPropertyKeyValues<TEntity>(TEntity entity)
            where TEntity : class
        {
            IEnumerable<string> keyNames = this.GetPropertyKeys<TEntity>(entity);

            List<object> pksValues = new List<object>();

            foreach (string key in keyNames)
            {
                pksValues.Add(typeof(TEntity).GetProperty(key).GetValue(entity));
            }

            return pksValues;
        }


        public decimal SqlQuerySingleDecimal(string query)
        {
            return this.Database.SqlQuery<decimal>(query).Single();
        }


        public DbRawSqlQuery<T> SQLQuery<T>(string sql, params object[] parameters)
        {
            return this.Database.SqlQuery<T>(sql, parameters);
        }


        public DbRawSqlQuery<T> SQLQuery<T>(string query)
        {
            return this.Database.SqlQuery<T>(query);
        }


        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                base.Dispose(disposing);
            }
        }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Mappings

            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new CategoriaMap());
            modelBuilder.Configurations.Add(new DespesaMap());
            modelBuilder.Configurations.Add(new LancamentoMap());
            modelBuilder.Configurations.Add(new OrcamentoMap());

            #endregion

        }

    }
}
