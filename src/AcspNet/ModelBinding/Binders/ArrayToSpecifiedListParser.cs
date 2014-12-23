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
		public static bool IsTypeValidForParsing(Type type)
		{
			if (type == typeof(IList<string>))
				return true;

			if (type == typeof(IList<int>))
				return true;

			if (type == typeof(IList<DateTime>))
				return true;

			if (type == typeof(IList<bool>))
				return true;

			if (type == typeof(IList<decimal>))
				return true;

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
			try
			{
				if (propertyInfo.PropertyType == typeof(IList<string>))
					return values.Select(StringToSpecifiedObjectParser.ParseString).ToList();

				if (propertyInfo.PropertyType == typeof(IList<int>))
					return values.Select(StringToSpecifiedObjectParser.ParseInt).ToList();

				if (propertyInfo.PropertyType == typeof(IList<DateTime>))
					return values.Select(x => StringToSpecifiedObjectParser.ParseDateTime(x, propertyInfo)).ToList();

				if (propertyInfo.PropertyType == typeof(IList<bool>))
					return values.Select(StringToSpecifiedObjectParser.ParseBool).ToList();

				if (propertyInfo.PropertyType == typeof(IList<decimal>))
					return values.Select(StringToSpecifiedObjectParser.ParseDecimal).ToList();
			}
			catch (ModelBindingException e)
			{
				throw new ModelBindingException(string.Format("Property '{0}' parsing failed", propertyInfo.Name), e);
			}

			throw new ModelBindingException(string.Format("Not supported property type: '{0}'", propertyInfo.PropertyType));
		}
	}
}
