namespace Recognized.Reflection
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;


    public delegate object GetProperty(object instance);

    /// <summary>
    /// A read-only property that can be read as an object from an object
    /// </summary>
    public class ReadOnlyProperty
    {
        /// <summary>
        /// The GetProperty method that returns the property
        /// </summary>
        public readonly GetProperty Get;

        /// <summary>
        /// The PropertyInfo for the property
        /// </summary>
        public readonly PropertyInfo Property;

        public ReadOnlyProperty(PropertyInfo property)
        {
            Property = property;
            Get = GetGetMethod(property);
        }

        static GetProperty GetGetMethod(PropertyInfo property)
        {
            ParameterExpression instance = Expression.Parameter(typeof(object), "instance");
            var typeInfo = property.DeclaringType.GetTypeInfo();
            if (typeInfo == null)
                throw new ReflectionException("Declaring type not found for property: " + property.Name);

            UnaryExpression instanceCast = typeInfo.IsValueType
                ? Expression.Convert(instance, typeInfo)
                : Expression.TypeAs(instance, typeInfo);

            MethodCallExpression call = Expression.Call(instanceCast, property.GetMethod);

            UnaryExpression typeAs = Expression.TypeAs(call, typeof(object));

            return Expression.Lambda<GetProperty>(typeAs, instance).Compile();
        }
    }


    public delegate object GetProperty<in T>(T instance);

    public class ReadOnlyProperty<T>
    {
        public readonly GetProperty<T> GetProperty;

        public ReadOnlyProperty(Expression<Func<T, object>> propertyExpression)
            : this(propertyExpression.GetPropertyInfo())
        {
        }

        public ReadOnlyProperty(PropertyInfo property)
        {
            Property = property;
            GetProperty = GetGetMethod(property);
        }

        public PropertyInfo Property { get; private set; }

        static GetProperty<T> GetGetMethod(PropertyInfo property)
        {
            ParameterExpression instance = Expression.Parameter(typeof(T), "instance");

            MethodCallExpression call = Expression.Call(instance, property.GetMethod);

            UnaryExpression typeAs = Expression.TypeAs(call, typeof(object));
            return Expression.Lambda<GetProperty<T>>(typeAs, instance).Compile();
        }
    }


    class ReadOnlyProperty<T, TProperty>
    {
        public readonly Func<T, TProperty> GetProperty;

        public ReadOnlyProperty(Expression<Func<T, object>> propertyExpression)
            : this(propertyExpression.GetPropertyInfo())
        {
        }

        public ReadOnlyProperty(PropertyInfo property)
        {
            Property = property;
            GetProperty = GetGetMethod(Property);
        }

        public PropertyInfo Property { get; private set; }

        public TProperty Get(T instance)
        {
            return GetProperty(instance);
        }

        static Func<T, TProperty> GetGetMethod(PropertyInfo property)
        {
            ParameterExpression instance = Expression.Parameter(typeof(T), "instance");

            MethodCallExpression call = Expression.Call(instance, property.GetMethod);

            return Expression.Lambda<Func<T, TProperty>>(call, instance).Compile();
        }
    }
}