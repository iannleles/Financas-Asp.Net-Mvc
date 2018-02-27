using System.Collections.Generic;
using System.Linq.Expressions;

namespace Financas.Domain.Common.Specifications
{
    internal class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        /// <summary>
        /// Método estático que ao ser invocado, instancia a própria classe ParameterRebinder
        /// Uma vez instanciada, a re-escrita do método VisitParameter será acionada.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static Expression ReplaceParameters(
            Dictionary<ParameterExpression, ParameterExpression> map,
            Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        /// <summary>
        /// Re-escrita do método VisitParameter, que garante a obtenção da expressão
        /// correta na invocação da Expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected override Expression VisitParameter(ParameterExpression expression)
        {
            ParameterExpression replacementExpression;
            if (map.TryGetValue(expression, out replacementExpression))
            {
                expression = replacementExpression;
            }
            return base.VisitParameter(expression);
        }
    }
}
