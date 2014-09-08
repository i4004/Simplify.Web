using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

				if (!int.TryParse(value, out buffer))
					return null;

				return buffer;
			}

			if (requiredDataType == typeof(bool))
				return value == "on";

			throw new ModelBindingException(string.Format("Not supported property type: '{0}'", requiredDataType.ToString()));
		}
	}
}