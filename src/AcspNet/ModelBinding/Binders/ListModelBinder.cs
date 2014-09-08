using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Simplify.String;

namespace AcspNet.ModelBinding.Binders
{
	/// <summary>
	/// Provides list of key value pair to model binding
	/// </summary>
	public static class ListModelBinder
	{
		private static readonly Type RequiredAttributeType = typeof(RequiredAttribute);

		/// <summary>
		/// Binds list to model.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T Bind<T>(IList<KeyValuePair<string, string>> source)
		{
			var type = typeof(T);

			var obj = Activator.CreateInstance<T>();

			foreach (var propInfo in type.GetProperties())
			{
				var propertyInfo = propInfo;
				propInfo.SetValue(obj, ParseProperty(propInfo, source.FirstOrDefault(x => x.Key == propertyInfo.Name)));
			}

			return obj;
		}

		private static object ParseProperty(PropertyInfo propertyInfo, KeyValuePair<string, string> keyValuePair)
		{
			var isRequired = propertyInfo.CustomAttributes.Any(x => x.AttributeType == RequiredAttributeType);

			string rawValue = null;

			if (!keyValuePair.Equals(default(KeyValuePair<string, string>)) && !string.IsNullOrEmpty(keyValuePair.Value))
				rawValue = keyValuePair.Value;

			if (isRequired && string.IsNullOrEmpty(rawValue))
				throw new ModelBindingException(string.Format("Required property '{0}' is null or empty", propertyInfo.Name));

			var parsedValue = ParseValue(rawValue, propertyInfo.PropertyType);

			if (propertyInfo.PropertyType == typeof (string))
				ValidateString(propertyInfo, rawValue);

			if (parsedValue == null && isRequired)
				throw new ModelBindingException(string.Format("Required property type is: '{0}', actual value: '{1}'", propertyInfo.PropertyType.ToString(), rawValue));

			return parsedValue;
		}

		private static object ParseValue(string value, Type requiredDataType)
		{
			if (requiredDataType == typeof(string))
				return value;

			if (requiredDataType == typeof(int))
			{
				int buffer;

				if (!int.TryParse(value, out buffer))
					return null;

				return buffer;
			}

			if (requiredDataType == typeof (bool))
				return ParseBool(value);

			throw new ModelBindingException(string.Format("Not supported property type: '{0}'", requiredDataType.ToString()));
		}

		private static void ValidateString(PropertyInfo propertyInfo, string value)
		{
			var attributes = propertyInfo.GetCustomAttributes(typeof(MinLengthAttribute), false);

			if (attributes.Length > 0)
			{
				var minLength = ((MinLengthAttribute) attributes[0]).MinimumPropertyLength;
				if (value.Length < minLength)
					throw new ModelBindingException(string.Format("Property '{0}' required minimum length is '{1}', actual value: '{2}'", propertyInfo.Name, minLength, value));
			}

			attributes = propertyInfo.GetCustomAttributes(typeof(MaxLengthAttribute), false);

			if (attributes.Length > 0)
			{
				var maxLength = ((MaxLengthAttribute)attributes[0]).MaximumPropertyLength;
				if (value.Length > maxLength)
					throw new ModelBindingException(string.Format("Property '{0}' required maximum length is '{1}', actual value: '{2}'", propertyInfo.Name, maxLength, value));
			}

			attributes = propertyInfo.GetCustomAttributes(typeof(EMailAttribute), false);

			if (attributes.Length > 0)
			{
				if (!StringHelper.ValidateEMail(value))
					throw new ModelBindingException(string.Format("Property '{0}' should be an email, actual value: '{1}'", propertyInfo.Name, value));
			}

			attributes = propertyInfo.GetCustomAttributes(typeof(RegexAttribute), false);

			if (attributes.Length > 0)
			{
				var regexString = ((RegexAttribute)attributes[0]).RegexString;

				if (!Regex.IsMatch(value, regexString))
					throw new ModelBindingException(string.Format("Property '{0}' regex not matched, actual value: '{1}', pattern: '{2}'", propertyInfo.Name, value, regexString));
			}
		}

		private static bool ParseBool(string value)
		{
			value = value.ToLower();

			if (value == "on")
				return true;

			return value == "true";
		}
	}
}