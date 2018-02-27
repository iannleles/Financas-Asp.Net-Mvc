using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Financas.Infrastructure.Data.Extensions
{
    public static class QueryableExtensions
    {
        //MÉTODO QUE RECEBE UMA QUERY COMO ENTRADA E
        //TAMBÉM RECEBE A PROPRIEDADE A SER UTILIZADA COMO ORDENAÇÃO
        //O MÉTODO RETORNA UMA NOVA QUERY COM A ORDENAÇÃO ASCENDENTE APLICADA
        public static IEnumerable<TEntity> OrderByAscendingByProperty<TEntity, TRet>(
            IEnumerable<TEntity> query,
            PropertyInfo prop)
        {
            //DECLARAMOS UMA EXPRESSION DE PARAMETROS PARA A
            //PRÓXIMA EXPRESSÃO A SER DECLARADA, QUE SERÁ A EXPRESSÃO
            //USADA NO OrderBy
            ParameterExpression paramOrderBy = Expression.Parameter(typeof(TEntity));

            //DECLARAMOS UMA EXPRESSION LINQ PARA UTILIZAR NO OrderBy
            //NESTE EXPRESSÃO VINCULAMOS A PROPRIEDADE PASSADA COMO PARÂMETRO DO MÉTODO
            Expression expressionSort = Expression.Convert(
                                            Expression.Property(paramOrderBy, prop),
                                                prop.PropertyType);

            //APLICAMOS NA QUERY RECEBIDA O ORDER BY PELA EXPRESSÃO CALCULADA
            return query.AsQueryable().OrderBy(
                Expression.Lambda<Func<TEntity, TRet>>(expressionSort, paramOrderBy));
        }

        //MÉTODO QUE RECEBE UMA QUERY COMO ENTRADA E
        //TAMBÉM RECEBE A PROPRIEDADE A SER UTILIZADA COMO ORDENAÇÃO
        //O MÉTODO RETORNA UMA NOVA QUERY COM A ORDENAÇÃO ASCENDENTE APLICADA
        public static IEnumerable<TEntity> OrderByDescendingByProperty<TEntity, TRet>(
            IEnumerable<TEntity> query,
            PropertyInfo prop)
        {
            //DECLARAMOS UMA EXPRESSION DE PARAMETROS PARA A
            //PRÓXIMA EXPRESSÃO A SER DECLARADA, QUE SERÁ A EXPRESSÃO
            //USADA NO OrderBy
            ParameterExpression paramOrderBy = Expression.Parameter(typeof(TEntity));

            //DECLARAMOS UMA EXPRESSION LINQ PARA UTILIZAR NO OrderBy
            //NESTE EXPRESSÃO VINCULAMOS A PROPRIEDADE PASSADA COMO PARÂMETRO DO MÉTODO
            Expression expressionSort = Expression.Convert(
                                            Expression.Property(paramOrderBy, prop),
                                                prop.PropertyType);

            //APLICAMOS NA QUERY RECEBIDA O ORDER BY PELA EXPRESSÃO CALCULADA
            return query.AsQueryable().OrderByDescending(
                Expression.Lambda<Func<TEntity, TRet>>(expressionSort, paramOrderBy));
        }

        //ESTE É DE FATO O MÉTODO QUE SERÁ A EXTENSÃO DO OrderBy
        public static IEnumerable<TEntity> OrderBy<TEntity>(
            this IEnumerable<TEntity> query,
            string propertyOrder)
        {
            //OBTER O TIPO DA ENTIDADE EM QUESTÃO
            Type typeEntity = typeof(TEntity);

            //OBTER A PROPRIEDADE À QUAL FOI SOLICITADA A ORDENAÇÃO
            PropertyInfo property = typeEntity.GetProperty(propertyOrder, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            try
            {
                //ATRAVÉS DE REFLEXÃO OBTEMOS O MÉTODO OrderByAcendenteByProperty
                //E NESTE MÉTODO 'OBTIDO' POR RELEXÃO, APLICAMOS OS PARÂMETROS GENERIC
                //OBS.: SOMENTE DESTA FORMA É POSSÍVEL PASSAR TIPOS OBTIDOS POR REFLEXÃO
                //      PARA MÉTODOS GENERIC
                MethodInfo methodOrderBy = typeof(QueryableExtensions)
                                            .GetMethod("OrderByAscendingByProperty")
                                            .MakeGenericMethod(typeEntity, property.PropertyType);

                //POR FIM INVOCAMOS O MÉTODO OBTIDO POR REFLEXÃO
                //O NULL INFORMADO É PORQUE O MÉTODO É ESTÁTICO
                //NO ARRAY object[] PASSAMOS OS PARÂMETROS DO MÉTODO
                //NESTE CASO, A QUERY E A PROPRIEDADE
                return (IEnumerable<TEntity>)methodOrderBy.Invoke(null, new object[] { query, property });
            }
            catch
            {
                return query;
            }
        }

        public static IEnumerable<TEntity> OrderByDescending<TEntity>(
            this IEnumerable<TEntity> query,
            string propertyOrder)
        {
            //OBTER O TIPO DA ENTIDADE EM QUESTÃO
            Type typeEntity = typeof(TEntity);

            //OBTER A PROPRIEDADE À QUAL FOI SOLICITADA A ORDENAÇÃO
            PropertyInfo property = typeEntity.GetProperty(propertyOrder, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            try
            {
                //ATRAVÉS DE REFLEXÃO OBTEMOS O MÉTODO OrderByDescendenteByProperty
                //E NESTE MÉTODO 'OBTIDO' POR RELEXÃO, APLICAMOS OS PARÂMETROS GENERIC
                //OBS.: SOMENTE DESTA FORMA É POSSÍVEL PASSAR TIPOS OBTIDOS POR REFLEXÃO
                //      PARA MÉTODOS GENERIC
                MethodInfo methodOrderBy = typeof(QueryableExtensions)
                                            .GetMethod("OrderByDescendingByProperty")
                                            .MakeGenericMethod(typeEntity, property.PropertyType);

                //POR FIM INVOCAMOS O MÉTODO OBTIDO POR REFLEXÃO
                //O NULL INFORMADO É PORQUE O MÉTODO É ESTÁTICO
                //NO ARRAY object[] PASSAMOS OS PARÂMETROS DO MÉTODO
                //NESTE CASO, A QUERY E A PROPRIEDADE
                return (IEnumerable<TEntity>)methodOrderBy.Invoke(null, new object[] { query, property });
            }
            catch
            {
                return query;
            }
        }

        public static IEnumerable<TEntity> ThenByAscendingByProperty<TEntity, TRet>(
            IOrderedQueryable<TEntity> queryOrdered,
            PropertyInfo property)
        {
            ParameterExpression paramOrderBy = Expression.Parameter(typeof(TEntity));

            Expression expressionSort = Expression.Convert(
                                            Expression.Property(paramOrderBy, property),
                                                property.PropertyType);

            return queryOrdered.ThenBy(
                Expression.Lambda<Func<TEntity, TRet>>(expressionSort, paramOrderBy));
        }

        public static IEnumerable<TEntity> ThenByDescendingByProperty<TEntity, TRet>(
            IOrderedQueryable<TEntity> queryOrdered,
            PropertyInfo property)
        {
            ParameterExpression paramOrderBy = Expression.Parameter(typeof(TEntity));

            Expression expressionSort = Expression.Convert(
                                            Expression.Property(paramOrderBy, property),
                                                property.PropertyType);

            return queryOrdered.ThenByDescending(
                Expression.Lambda<Func<TEntity, TRet>>(expressionSort, paramOrderBy));
        }

        public static IEnumerable<TEntity> ThenBy<TEntity>(
            this IOrderedQueryable<TEntity> queryOrdered,
            string propertyOrder)
        {
            Type typeEntity = typeof(TEntity);

            PropertyInfo property = typeEntity.GetProperty(propertyOrder, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            MethodInfo methodThenBy = typeof(QueryableExtensions)
                                        .GetMethod("ThenByAscendingByProperty")
                                        .MakeGenericMethod(typeEntity, property.PropertyType);

            return (IEnumerable<TEntity>)methodThenBy.Invoke(null, new object[] { queryOrdered, property });
        }

        public static IEnumerable<TEntity> ThenByDescending<TEntity>(
            this IOrderedQueryable<TEntity> queryOrdered,
            string propertyOrder)
        {
            Type typeEntity = typeof(TEntity);

            PropertyInfo property = typeEntity.GetProperty(propertyOrder, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            MethodInfo methodThenBy = typeof(QueryableExtensions)
                                        .GetMethod("ThenByDescendingByProperty")
                                        .MakeGenericMethod(typeEntity, property.PropertyType);

            return (IEnumerable<TEntity>)methodThenBy.Invoke(null, new object[] { queryOrdered, property });
        }
    }
}
