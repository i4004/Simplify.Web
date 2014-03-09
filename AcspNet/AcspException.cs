using System;

namespace AcspNet
{
	/// <summary>
	/// Represent ACSP related exceptions
	/// </summary>
	[Serializable]
	public sealed class AcspException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AcspException" /> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public AcspException(string message) : base(message)
		{
		}
	}
}