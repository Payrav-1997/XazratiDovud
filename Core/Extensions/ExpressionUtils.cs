using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    internal static class ExpressionUtils
    {
		public static Expression GetProperty(Expression expression, string path)
		{
			var delimiter = '.';
			var properties = path.Split(delimiter);
			var property = properties[0];

			// Try get PropertyInfo
			var propertyInfo = expression.Type
				.GetProperty(property, BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance);

			// Check if property exists
			if (propertyInfo == null)
			{
				// Return error
				throw new MemberAccessException($"Property {property} does not exists on type {expression.Type}.");
			}

			// Create member access expression
			expression = Expression.Property(expression, propertyInfo);

            // Check if have deeper navigation properties



            //-------------------------



            if (properties.Length > 1)
            {
                return GetProperty(expression, properties.Skip(1).Implode(delimiter));
            }

            // Return created expression
            return expression;
		}





		/// <summary>
		/// Returns <see cref="Expression"/> which normalizes <see cref="String"/> value,
		/// by sequentially calling <see cref="Object.ToString()"/>, <see cref="String.ToUpper()"/>
		/// and latelly <see cref="String.Trim()"/>.
		/// </summary>
		/// <param name="expression">Source expression</param>
		/// <returns><see cref="MethodCallExpression"/></returns>
		/// 
		private static Expression GetNormalized(Expression expression)
		{
			if (expression.Type != typeof(String))
			{
				expression = Expression.Call(expression, "ToString", null);
			}

			expression = Expression.Call(expression, "ToUpper", null);
			expression = Expression.Call(expression, "Trim", null);

			return expression;
		}

	}
}
