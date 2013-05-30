
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;

namespace Membrane.DataTransfer.Projection
{
    /// <summary>
    /// 
    /// </summary>
    public static class FluentProjection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="property"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static TEntity Project<TEntity>(this TEntity entity, Expression<Func<TEntity, object>> property, Func<ExpandoObject> provider)
        {
            return entity.Project<TEntity>(property, provider, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="property"></param>
        /// <param name="provider"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public static TEntity Project<TEntity>(this TEntity entity, Expression<Func<TEntity, object>> property, Func<ExpandoObject> provider, Func<object> converter)
        {
            PropertyInfo propertyInfo = null;
            if (property.Body is MemberExpression)
            {
                propertyInfo = (property.Body as MemberExpression).Member as PropertyInfo;
            }
            else
            {
                propertyInfo = (((UnaryExpression)property.Body).Operand as MemberExpression).Member as PropertyInfo;
            }

            // Convert the ExpandoObject object to a dictionary representation.
            IDictionary<string, object> data = (IDictionary<string, object>)provider.Invoke();

            // Extract the value from the dictionary.
            object dataValue;
            if (data.TryGetValue(propertyInfo.Name, out dataValue))
            {
                object value = converter == null ? dataValue : converter.Invoke();
                propertyInfo.SetValue(entity, value, null);
            }

            return entity;
        }
    }
}
