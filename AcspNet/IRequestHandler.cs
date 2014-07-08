using System.Threading.Tasks;
using Microsoft.Owin;

namespace AcspNet
{
	/// <summary>
	/// Represent OWIN HTTP request handler
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