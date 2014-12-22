using System;
using System.Globalization;
using System.Reflection;
using AcspNet.ModelBinding.Attributes;

namespace AcspNet.ModelBinding.Binders
{
	/// <summary>
	/// Provides string to property type object parsing
	/// </summary>
	public class StringParser
	{
		/// <summary>
		/// Determine variable type and parses, validates it from string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="propertyInfo">The property information.</param>
		/// <returns></returns>
		/// <exception cref="AcspNet.ModelBinding.ModelBindingException"></exception>
		public static object ParseUndefined(string value, PropertyInfo propertyInfo)
		{
			try
			{
				if (propertyInfo.PropertyType == typeof(string))
					return ParseString(value, propertyInfo);

				if (propertyInfo.PropertyType == typeof(int))
					return ParseInt(value);

				if (propertyInfo.PropertyType == typeof(int?))
					return ParseNullableInt(value);

				if (propertyInfo.PropertyType == typeof(bool))
					return ParseBool(value);

				if (propertyInfo.PropertyType == typeof(bool?))
					return ParseNullableBool(value);

				if (propertyInfo.PropertyType == typeof(decimal))
					return ParseDecimal(value);

				if (propertyInfo.PropertyType == typeof(decimal?))
					return ParseNullableDecimal(value);

				if (propertyInfo.PropertyType == typeof(DateTime))
					return ParseDateTime(propertyInfo, value);

				if (propertyInfo.PropertyType == typeof(DateTime?))
					return ParseNullableDateTime(propertyInfo, value);
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
		/// <param name="propertyInfo">The property information.</param>
		/// <returns></returns>
		public static string ParseString(string value, PropertyInfo propertyInfo)
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
		/// <param name="propertyInfo">The property information.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		/// <exception cref="AcspNet.ModelBinding.ModelBindingException">
		/// </exception>
		public static DateTime ParseDateTime(PropertyInfo propertyInfo, string value)
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
		/// <param name="propertyInfo">The property information.</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		/// <exception cref="AcspNet.ModelBinding.ModelBindingException">
		/// </exception>
		public static DateTime? ParseNullableDateTime(PropertyInfo propertyInfo, string value)
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
	}
}