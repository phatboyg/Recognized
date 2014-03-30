namespace Recognized.Reflection
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;


    public static class PropertyExpressionExtensions
    {
        /// <summary>
        ///     Return the PropertyInfo for an expression that references the property of a type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TMember"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static PropertyInfo GetPropertyInfo<T, TMember>(this Expression<Func<T, TMember>> expression)
        {
            var propertyInfo = expression.GetMemberExpression().Member as PropertyInfo;
            if (propertyInfo == null)
                throw new ArgumentException("The member is not a property");

            return propertyInfo;
        }


        public static MemberExpression GetMemberExpression<T, TMember>(this Expression<Func<T, TMember>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            return GetMemberExpression(expression.Body);
        }

        static MemberExpression GetMemberExpression(Expression body)
        {
            if (body == null)
                throw new ArgumentNullException("body");

            MemberExpression memberExpression = null;
            if (body.NodeType == ExpressionType.Convert)
            {
                var unaryExpression = (UnaryExpression) body;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else if (body.NodeType == ExpressionType.MemberAccess)
                memberExpression = body as MemberExpression;

            if (memberExpression == null)
                throw new ArgumentException("Expression is not a member expression");

            return memberExpression;
        }
    }
}