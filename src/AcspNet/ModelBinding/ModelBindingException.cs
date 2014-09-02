using System;

namespace AcspNet.ModelBinding
{
	/// <summary>
	/// Represent model binding exceptions
	/// </summary>
	public class ModelBindingException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ModelBindingException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public ModelBindingException(string message) : base(message)
		{
		}
	}
}