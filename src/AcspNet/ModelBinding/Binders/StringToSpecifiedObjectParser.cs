using System;
using System.Globalization;
using System.Reflection;
using AcspNet.ModelBinding.Attributes;

namespace AcspNet.ModelBinding.Binders
{
	/// <summary>
	/// Provides string to property type object parsing
	/// </summary>
	public class StringToSpecifiedObjectParser
	{
		/// <summary>
		/// Determine variable type and parses, validates it from string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="parsingType">Type of the parsing.</param>
		/// <param name="propertyInfo">The property information.</param>
		/// <returns></returns>
		/// <exception cref="ModelBindingException">
		/// </exception>
		/// <exception cref="AcspNet.ModelBinding.ModelBindingException"></exception>
		public static object ParseUndefined(string value, Type parsingType, PropertyInfo propertyInfo)
		{
			try
			{
				if (parsingType == typeof(string))
					return ParseString(value);

				if (parsingType == typeof(int))
					return ParseInt(value);

				if (parsingType == typeof(int?))
					return ParseNullableInt(value);

				if (parsingType == typeof(bool))
					return ParseBool(value);

				if (parsingType == typeof(bool?))
					return ParseNullableBool(value);

				if (parsingType == typeof(decimal))
					return ParseDecimal(value);

				if (parsingType == typeof(decimal?))
					return ParseNullableDecimal(value);

				if (parsingType == typeof(DateTime))
					return ParseDateTime(value, propertyInfo);

				if (parsingType == typeof(DateTime?))
					return ParseNullableDateTime(value, propertyInfo);

				if (parsingType.IsEnum)
					return ParseEnum(value, propertyInfo.PropertyType);
			}
			catch (ModelBindingException e)
			{
				throw new ModelBindingException(string.Format("Property '{0}' parsing failed", propertyInfo.Name), e);
			}

			throw new ModelBindingException(string.Format("Not supported property type: '{0}'", propertyInfo.PropertyType));
		}

		/// <summary>
		/// Parses the string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static string ParseString(string value)
		{
			return string.IsNullOrEmpty(value) ? null : value;
		}

		/// <summary>
		/// Parses the boolean from string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static bool ParseBool(string value)
		{
			if (string.IsNullOrEmpty(value))
				return default(bool);

			value = value.ToLower();

			if (value == "on")
				return true;

			bool buffer;

			if (bool.TryParse(value, out buffer))
				return buffer;

			throw new ModelBindingException(string.Format("Bool property parse failed, actual value: '{0}'", value));
		}

		/// <summary>
		/// Parses the nullable boolean from string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static bool? ParseNullableBool(string value)
		{
			if (string.IsNullOrEmpty(value))
				return null;

			value = value.ToLower();

			if (value == "on")
				return true;

			bool buffer;

			if (bool.TryParse(value, out buffer))
				return buffer;

			throw new ModelBindingException(string.Format("Bool property parse failed, actual value: '{0}'", value));
		}

		/// <summary>
		/// Parses the int from string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		/// <exception cref="AcspNet.ModelBinding.ModelBindingException"></exception>
		public static int ParseInt(string value)
		{
			if (string.IsNullOrEmpty(value))
				return default(int);

			int buffer;

			if (int.TryParse(value, out buffer))
				return buffer;

			throw new ModelBindingException(string.Format("Int property parse failed, actual value: '{0}'", value));
		}

		/// <summary>
		/// Parses the nullable int from string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		/// <exception cref="AcspNet.ModelBinding.ModelBindingException"></exception>
		public static int? ParseNullableInt(string value)
		{
			if (string.IsNullOrEmpty(value))
				return null;

			int buffer;

			if (int.TryParse(value, out buffer))
				return buffer;

			throw new ModelBindingException(string.Format("Int property parse failed, actual value: '{0}'", value));
		}

		/// <summary>
		/// Parses the decimal from string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		/// <exception cref="AcspNet.ModelBinding.ModelBindingException"></exception>
		public static decimal ParseDecimal(string value)
		{
			if (string.IsNullOrEmpty(value))
				return default(decimal);

			decimal buffer;

			if (decimal.TryParse(value, out buffer))
				return buffer;

			throw new ModelBindingException(string.Format("Decimal property parse failed, actual value: '{0}'", value));
		}

		/// <summary>
		/// Parses the nullable decimal from string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		/// <exception cref="AcspNet.ModelBinding.ModelBindingException"></exception>
		public static decimal? ParseNullableDecimal(string value)
		{
			if (string.IsNullOrEmpty(value))
				return null;

			decimal buffer;

			if (decimal.TryParse(value, out buffer))
				return buffer;

			throw new ModelBindingException(string.Format("Decimal property parse failed, actual value: '{0}'", value));
		}

		/// <summary>
		/// Parses the date time from string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="propertyInfo">The property information.</param>
		/// <returns></returns>
		/// <exception cref="AcspNet.ModelBinding.ModelBindingException">
		/// </exception>
		public static DateTime ParseDateTime(string value, PropertyInfo propertyInfo)
		{
			if (string.IsNullOrEmpty(value))
				return default(DateTime);

			var attributes = propertyInfo.GetCustomAttributes(typeof(DateTimeFormatAttribute), false);
			DateTime buffer;

			if (attributes.Length > 0)
			{
				var format = ((DateTimeFormatAttribute)attributes[0]).Format;

				if (DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out buffer))
					return buffer;

				throw new ModelBindingException(string.Format("DateTime property '{0}' required format is '{1}', actual value: '{2}'", propertyInfo.Name, format, value));
			}

			if (DateTime.TryParse(value, out buffer))
				return buffer;

			throw new ModelBindingException(string.Format("DateTime property '{0}' parsing failed, actual value: '{1}'", propertyInfo.Name, value));
		}

		/// <summary>
		/// Parses the date time from string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="propertyInfo">The property information.</param>
		/// <returns></returns>
		/// <exception cref="AcspNet.ModelBinding.ModelBindingException">
		/// </exception>
		public static DateTime? ParseNullableDateTime(string value, PropertyInfo propertyInfo)
		{
			if (string.IsNullOrEmpty(value))
				return null;

			var attributes = propertyInfo.GetCustomAttributes(typeof(DateTimeFormatAttribute), false);
			DateTime buffer;

			if (attributes.Length > 0)
			{
				var format = ((DateTimeFormatAttribute)attributes[0]).Format;

				if (DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out buffer))
					return buffer;

				throw new ModelBindingException(string.Format("DateTime property '{0}' required format is '{1}', actual value: '{2}'", propertyInfo.Name, format, value));
			}

			if (DateTime.TryParse(value, out buffer))
				return buffer;

			throw new ModelBindingException(string.Format("DateTime property '{0}' parsing failed, actual value: '{1}'", propertyInfo.Name, value));
		}

		/// <summary>
		/// Parses the enum.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="enumType">Type of the enum.</param>
		/// <returns></returns>
		public static object ParseEnum(string value, Type enumType)
		{
			return Enum.Parse(enumType, value);
		}

		/// <summary>
		/// Determines whether specified type is valid for parsing.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public static bool IsTypeValidForParsing(Type type)
		{
			if (type == typeof (string))
				return true;

			if (type == typeof(int))
				return true;

			if (type == typeof(int?))
				return true;

			if (type == typeof(bool))
				return true;

			if (type == typeof(bool?))
				return true;

			if (type == typeof(decimal))
				return true;

			if (type == typeof(decimal?))
				return true;

			if (type == typeof(DateTime))
				return true;

			if (type == typeof(DateTime?))
				return true;

			if (type.IsEnum)
				return true;

			return false;
		}
	}
}