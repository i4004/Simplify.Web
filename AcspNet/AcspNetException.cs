using System;

namespace AcspNet
{
	/// <summary>
	/// The exception class using for ACSP.NET exceptions
	/// </summary>
	[Serializable]
	public sealed class AcspNetException : Exception
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="AcspNetException" /> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public AcspNetException(string message) : base(message)
		{
		}
	}
}