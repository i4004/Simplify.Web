using System.Threading.Tasks;
using Microsoft.Owin;

namespace AcspNet.Core
{
	/// <summary>
	/// Represent OWIN HTTP request handler
	/// </summary>
	public interface IRequestHandler
	{
		/// <summary>
		/// Processes the OWIN HTTP request.
		/// </summary>
		/// <param name="context">The context.</param>
		Task ProcessRequest(IOwinContext context);
	}
}