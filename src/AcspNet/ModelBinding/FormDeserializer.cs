using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Owin;

namespace AcspNet.ModelBinding
{
	/// <summary>
	/// Provides form data to object deserialization
	/// </summary>
	public static class FormDeserializer
	{
		private static readonly Type RequiredAttributeType = typeof(RequiredAttribute);

		/// <summary>
		/// Deserializes this instance.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T Deserialize<T>(IFormCollection form)
		{
			var type = typeof(T);

			var obj = Activator.CreateInstance<T>();

			foreach (var propInfo in type.GetProperties())
				propInfo.SetValue(obj, ParseProperty(propInfo, form));

			return obj;
		}

		private static object ParseProperty(PropertyInfo propertyInfo, IEnumerable<KeyValuePair<string, string[]>> form)
		{
			var isRequired = propertyInfo.CustomAttributes.Any(x => x.AttributeType == RequiredAttributeType);
			var formField = form.FirstOrDefault(x => x.Key == propertyInfo.Name);

			string rawValue = null;

			if (!formField.Equals(default(KeyValuePair<string, string[]>)) && !string.IsNullOrEmpty(formField.Value[0]))
				rawValue = formField.Value[0];

			if (isRequired && string.IsNullOrEmpty(rawValue))
				throw new ModelBindingException(string.Format("Required parameter '{0}' is null or empty", propertyInfo.Name));

			var parsedValue = ParseValue(rawValue, propertyInfo.PropertyType);

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

				if (int.TryParse(value, out buffer))
					return null;

				return buffer;
			}

			if (requiredDataType == typeof(bool))
				return value == "on";

			return value;
		}
	}
}