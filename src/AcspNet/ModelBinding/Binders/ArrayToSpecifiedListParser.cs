using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AcspNet.ModelBinding.Binders
{
	/// <summary>
	/// Provides string array to list parsing
	/// </summary>
	public class ArrayToSpecifiedListParser
	{
		/// <summary>
		/// Determines whether specified type is valid for parsing.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		/// <exception cref="ModelBindingException">Exception throws in case of underfined list type</exception>
		public static bool IsTypeValidForParsing(Type type)
		{
			if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof (IList<>))
				return false;

			var genericType = GetGenericListType(type);

			if (genericType != null)
			{
				if (!StringToSpecifiedObjectParser.IsTypeValidForParsing(genericType))
					throw new ModelBindingException(string.Format("Not supported list property type of: '{0}'", genericType));

				return true;
			}

			return false;
		}

		/// <summary>
		/// Parses the undefined values types from string array to list.
		/// </summary>
		/// <param name="values">The values.</param>
		/// <param name="propertyInfo">The property information.</param>
		/// <returns></returns>
		/// <exception cref="ModelBindingException">
		/// </exception>
		public static object ParseUndefined(string[] values, PropertyInfo propertyInfo)
		{
			var parsingType = GetGenericListType(propertyInfo.PropertyType);

			if (parsingType == typeof(string))
				return values.Select(StringToSpecifiedObjectParser.ParseString).ToList();

			if (parsingType == typeof(int))
				return values.Select(StringToSpecifiedObjectParser.ParseInt).ToList();

			if (parsingType == typeof(int?))
				return values.Select(StringToSpecifiedObjectParser.ParseNullableInt).ToList();

			if (parsingType == typeof(bool))
				return values.Select(StringToSpecifiedObjectParser.ParseBool).ToList();

			if (parsingType == typeof(bool?))
				return values.Select(StringToSpecifiedObjectParser.ParseNullableBool).ToList();

			if (parsingType == typeof(decimal))
				return values.Select(StringToSpecifiedObjectParser.ParseDecimal).ToList();
			
			if (parsingType == typeof(decimal?))
				return values.Select(StringToSpecifiedObjectParser.ParseNullableDecimal).ToList();
			
			if (parsingType == typeof(DateTime))
				return values.Select(x => StringToSpecifiedObjectParser.ParseDateTime(x, propertyInfo)).ToList();

			if (parsingType == typeof(DateTime?))
				return values.Select(x => StringToSpecifiedObjectParser.ParseNullableDateTime(x, propertyInfo)).ToList();

			if (parsingType.IsEnum)
			{
				var listType = typeof (List<>).MakeGenericType(parsingType);
				var list = Activator.CreateInstance(listType);
				var methodInfo = listType.GetMethod("Add");

				foreach (var value in values)
					methodInfo.Invoke(list, new[] {StringToSpecifiedObjectParser.ParseEnum(value, parsingType)});

				return list;
			}

			throw new ModelBindingException(string.Format("Not supported property type: '{0}'", propertyInfo.PropertyType));
		}

		/// <summary>
		/// Gets the type of the generic list.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public static Type GetGenericListType(Type type)
		{
			var genericTypeArguments = type.GetTypeInfo().GenericTypeArguments;

			return genericTypeArguments.Length > 0 ? genericTypeArguments[0] : null;
		}
	}
}
