using Microsoft.Owin;

namespace AcspNet.Modules
{
	/// <summary>
	/// Represent AcspNet context
	/// </summary>
	public interface IAcspNetContext
	{
		/// <summary>
		/// Gets the context for the current HTTP request.
		/// </summary>
		IOwinContext Context { get; }
	}
}