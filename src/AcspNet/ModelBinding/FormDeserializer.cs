using System;
using System.Linq;
using Microsoft.Owin;

namespace AcspNet.ModelBinding
{
	/// <summary>
	/// Provides form data to object deserialization
	/// </summary>
	public static class FormDeserializer
	{
		/// <summary>
		/// Deserializes this instance.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public static T Deserialize<T>(IFormCollection form)
		{
			var type = typeof (T);
			var requiredAttributeType = typeof(RequiredAttribute);

			var obj = Activator.CreateInstance<T>();

			foreach (var propInfo in type.GetProperties())
			{
				var propertyInfo = propInfo;
				var isRequired = propInfo.CustomAttributes.Any(x => x.AttributeType == requiredAttributeType);

				var formField = form.FirstOrDefault(x => x.Key == propertyInfo.Name);

				var value = !string.IsNullOrEmpty(formField.Value[0]) ? formField.Value[0] : null;

				ParseProperty(isRequired, value);
			}

			return obj;
		}

		private static object ParseProperty(bool isRequired, string value)
		{			
			throw new NotImplementedException();
		}
	}
}