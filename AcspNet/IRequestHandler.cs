using System.Threading.Tasks;
using Microsoft.Owin;

namespace AcspNet
{
	/// <summary>
	/// Provides HTTP request handler
	/// </summary>
	public interface IRequestHandler
	{
		/// <summary>
		/// Processes the request.
		/// </summary>
		/// <param name="context">The context.</param>
		Task ProcessRequest(IOwinContext context);
	}
}