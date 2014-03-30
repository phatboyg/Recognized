namespace Recognized.Reflection
{
    using System;
    using System.Reflection;


    public static class TypeExtensions
    {
        /// <summary>
        ///     Determines if a type is neither abstract nor an interface and can be constructed.
        /// </summary>
        /// <param name="type">The type to check</param>
        /// <returns>True if the type can be constructed, otherwise false.</returns>
        public static bool IsConcrete(this Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return !typeInfo.IsAbstract && !typeInfo.IsInterface;
        }

        /// <summary>
        ///     Determines if a type is neither abstract nor an interface and can be constructed.
        /// </summary>
        /// <param name="typeInfo">The type to check</param>
        /// <returns>True if the type can be constructed, otherwise false.</returns>
        public static bool IsConcrete(this TypeInfo typeInfo)
        {
            return !typeInfo.IsAbstract && !typeInfo.IsInterface;
        }

        /// <summary>
        ///     Determines if the type is a nullable type
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>True if the type can be null</returns>
        public static bool IsNullable(this Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }

        /// <summary>
        ///     Determines if the type is a nullable type
        /// </summary>
        /// <param name="type">The type</param>
        /// <param name="underlyingType">The underlying type of the nullable</param>
        /// <returns>True if the type can be null</returns>
        public static bool IsNullable(this Type type, out Type underlyingType)
        {
            underlyingType = Nullable.GetUnderlyingType(type);
            return underlyingType != null;
        }

        public static bool AllowsNull(this Type type)
        {
            return !type.IsValueType || IsNullable(type);
        }

        public static bool IsSimple(this Type type)
        {
            return type.IsPrimitive
                   || type == typeof(String)
                   || type == typeof(DateTime)
                   || type == typeof(DateTimeOffset)
                   || type == typeof(TimeSpan)
                   || type == typeof(Decimal)
                   || type == typeof(Guid);
        }

        public static bool IsSimpleUnderlyingType(this Type type)
        {
            Type underlyingType = Nullable.GetUnderlyingType(type);
            if (underlyingType == null)
            {
                return IsSimple(type);
            }

            return IsSimple(underlyingType);
        }
    }
}