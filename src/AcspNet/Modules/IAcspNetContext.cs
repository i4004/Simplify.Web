using Microsoft.Owin;

namespace AcspNet.Modules
{
	/// <summary>
	/// Represent AcspNet context
	/// </summary>
	public interface IAcspNetContext : IHideObjectMembers
	{
		/// <summary>
		/// Site root url, for example: http://mysite.com or http://localhost/mysite/
		/// </summary>
		string SiteUrl { get; }
		
		/// <summary>
		/// Gets the virtual path.
		/// </summary>
		/// <value>
		/// The virtual path.
		/// </value>
		string VirtualPath { get; }

		/// <summary>
		/// Gets the context for the current HTTP request.
		/// </summary>
		IOwinContext Context { get; }

		/// <summary>
		/// Gets the request for the current HTTP request.
		/// </summary>
		IOwinRequest Request { get; }

		/// <summary>
		/// Gets the response for the current HTTP request.
		/// </summary>
		IOwinResponse Response { get; }
		
		/// <summary>
		/// Gets the query string for current HTTP request.
		/// </summary>
		IReadableStringCollection Query { get; }

		/// <summary>
		/// Gets the form data of post HTTP request.
		/// </summary>
		IFormCollection Form { get; }
	}
}