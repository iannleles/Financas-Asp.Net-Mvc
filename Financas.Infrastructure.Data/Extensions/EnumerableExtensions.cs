using System.Collections.Generic;
using System.Linq;
using Financas.Domain.ValueObjects;


namespace Financas.Infrastructure.Data.Extensions
{
    public static class EnumerableExtensions
    {
        public static IQueryable<TEntity> ApplyMultipleOrders<TEntity>(
            this IEnumerable<TEntity> DbSet,
            params SortField[] ordersFields)
        {
            bool firstProperty = true;

            IQueryable<TEntity> newQuery = DbSet.AsQueryable();

            foreach (SortField clausulaOrderBy in ordersFields)
            {
                string propertyOrder = clausulaOrderBy.Field;
                SortFieldDirection propertyDirection = clausulaOrderBy.Direction;

                //NA PRIMEIRA ORDENAÇÃO USAMOS O OrderBy e OrderByDescending
                if (firstProperty)
                {
                    if (propertyDirection == SortFieldDirection.Ascending)
                    {
                        //USAMOS O MÉTODO ESTENDIDO DE OrderBy
                        newQuery = (IOrderedQueryable<TEntity>)DbSet.OrderBy(propertyOrder);
                    }
                    else
                    {
                        //USAMOS O MÉTODO ESTENDIDO DE OrderByDescending
                        newQuery = (IOrderedQueryable<TEntity>)DbSet.OrderByDescending(propertyOrder);
                    }

                    firstProperty = false;
                }
                //DA SEGUNDA ORDENAÇÃO EM DIANTE USAMOS O ThenBy e ThenByDescending
                else
                {
                    if (propertyDirection == SortFieldDirection.Ascending)
                    {
                        //USAMOS O MÉTODO ESTENDIDO DE OrderBy
                        newQuery = ((IOrderedQueryable<TEntity>)newQuery).ThenBy(propertyOrder).AsQueryable();
                    }
                    else
                    {
                        //USAMOS O MÉTODO ESTENDIDO DE OrderByDescending
                        newQuery = ((IOrderedQueryable<TEntity>)newQuery).ThenByDescending(propertyOrder).AsQueryable();
                    }
                }
            }

            return newQuery;
        }
    }
}
