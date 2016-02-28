using System.Reflection;
using System.Text.RegularExpressions;
using Simplify.String;
using Simplify.Web.ModelBinding.Attributes;

namespace Simplify.Web.ModelBinding.Validation
{
	/// <summary>
	/// Validates string using specified rules in attributes
	/// </summary>
	public class StringValidator
	{
		/// <summary>
		/// Validates the string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="propertyInfo">The property information.</param>
		/// <exception cref="ModelValidationException">
		/// </exception>
		public static void Validate(string value, PropertyInfo propertyInfo)
		{
			var attributes = propertyInfo.GetCustomAttributes(typeof(MinLengthAttribute), false);

			if (attributes.Length > 0)
			{
				var minLength = ((MinLengthAttribute)attributes[0]).MinimumPropertyLength;
				if (string.IsNullOrEmpty(value) || value.Length < minLength)
					throw new ModelValidationException(string.Format("Property '{0}' required minimum length is '{1}', actual value: '{2}'", propertyInfo.Name, minLength, value));
			}

			attributes = propertyInfo.GetCustomAttributes(typeof(MaxLengthAttribute), false);

			if (attributes.Length > 0)
			{
				var maxLength = ((MaxLengthAttribute)attributes[0]).MaximumPropertyLength;
				if (string.IsNullOrEmpty(value) || value.Length > maxLength)
					throw new ModelValidationException(string.Format("Property '{0}' required maximum length is '{1}', actual value: '{2}'", propertyInfo.Name, maxLength, value));
			}

			attributes = propertyInfo.GetCustomAttributes(typeof(EMailAttribute), false);

			if (attributes.Length > 0)
			{
				if (!StringHelper.ValidateEMail(value))
					throw new ModelValidationException(string.Format("Property '{0}' should be an email, actual value: '{1}'", propertyInfo.Name, value));
			}

			attributes = propertyInfo.GetCustomAttributes(typeof(RegexAttribute), false);

			if (attributes.Length > 0)
			{
				var regexString = ((RegexAttribute)attributes[0]).RegexString;

				if (string.IsNullOrEmpty(value) || !Regex.IsMatch(value, regexString))
					throw new ModelValidationException(string.Format("Property '{0}' regex not matched, actual value: '{1}', pattern: '{2}'", propertyInfo.Name, value, regexString));
			}
		}
	}
}