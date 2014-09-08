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
				propInfo.SetValue(obj, BindProperty(propInfo, source.FirstOrDefault(x => x.Key == propertyInfo.Name)));
			}

			return obj;
		}

		private static object BindProperty(PropertyInfo propertyInfo, KeyValuePair<string, string> keyValuePair)
		{
			var isRequired = propertyInfo.CustomAttributes.Any(x => x.AttributeType == RequiredAttributeType);

			string rawValue = null;

			if (!keyValuePair.Equals(default(KeyValuePair<string, string>)) && !string.IsNullOrEmpty(keyValuePair.Value))
				rawValue = keyValuePair.Value;

			if (isRequired && string.IsNullOrEmpty(rawValue))
				throw new ModelBindingException(string.Format("Required property '{0}' is null or empty", propertyInfo.Name));

			var parsedValue = DataParser.ParseUndefined(rawValue, propertyInfo);

			return parsedValue;
		}
	}
}