using System;
using System.Linq;
using System.Reflection;
using AcspNet.ModelBinding.Attributes;

namespace AcspNet.ModelBinding.Validation
{
	/// <summary>
	/// Provides object properties validator
	/// </summary>
	public class ObjectPropertiesValidator : IModelValidator
	{
		private static readonly Type RequiredAttributeType = typeof(RequiredAttribute);

		/// <summary>
		/// Validates the specified model.
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="model">The model.</param>
		/// <exception cref="ModelBindingException"></exception>
		public void Validate<T>(T model)
		{
			var type = typeof(T);

			foreach (var propInfo in type.GetProperties())
			{
				var isRequired = propInfo.CustomAttributes.Any(x => x.AttributeType == RequiredAttributeType);

				if (isRequired && !Validate(propInfo.GetValue(model), propInfo))
					throw new ModelBindingException(String.Format("Required property '{0}' is null or empty", propInfo.Name));
			}
		}

		/// <summary>
		/// Validates the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="propertyInfo">The property information.</param>
		/// <returns></returns>
		/// <exception cref="ModelBindingException"></exception>
		private static bool Validate(object value, PropertyInfo propertyInfo)
		{
			if (propertyInfo.PropertyType == typeof(string))
			{
				StringValidator.Validate((string)value, propertyInfo);

				return !string.IsNullOrEmpty((string)value);
			}

			if (propertyInfo.PropertyType == typeof(int?))
				return ((int?)value) != null;

			if (propertyInfo.PropertyType == typeof (bool?))
				return (((bool?) value) != null);

			if (propertyInfo.PropertyType == typeof (decimal?))
				return (((decimal?) value) != null);

			if (propertyInfo.PropertyType == typeof(DateTime))
				return (((DateTime)value) != default(DateTime));

			if (propertyInfo.PropertyType == typeof(DateTime?))
				return (((DateTime?)value) != null);

			throw new ModelBindingException(string.Format("Not supported property type: '{0}'", propertyInfo.PropertyType.ToString()));
		}
	}
}