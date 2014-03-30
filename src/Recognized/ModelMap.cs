namespace Recognized
{
    using System;
    using System.Linq.Expressions;


    public abstract class ModelMap<TModel>
    {
        protected void Map<TProperty>(Expression<Func<TModel, TProperty>> propertyExpression)
            where TProperty : class
        {
        }

        protected void Map<TProperty>(Expression<Func<TModel, TProperty?>> propertyExpression)
            where TProperty : struct
        {
        }

        protected void Map(Expression<Func<TModel, string>> propertyExpression)
        {
        }


        protected void Map<TProperty>(Expression<Func<TModel, Value<TProperty>>> propertyExpression)
        {
        }
    }
}