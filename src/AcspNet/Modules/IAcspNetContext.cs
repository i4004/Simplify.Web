using Microsoft.Owin;

namespace AcspNet.Modules
{
	/// <summary>
	/// Represent AcspNetContext
	/// </summary>
	public interface IAcspNetContext
	{
		/// <summary>
		/// Gets the request for the current HTTP request.
		/// </summary>
		IOwinRequest Request { get; }

		/// <summary>
		/// Gets the query string for current HTTP request.
		/// </summary>
		IReadableStringCollection Query { get; }
	}
}