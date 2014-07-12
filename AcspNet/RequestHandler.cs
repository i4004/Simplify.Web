using System.Threading.Tasks;
using Microsoft.Owin;

namespace AcspNet
{
	/// <summary>
	/// Provides OWIN HTTP request handler
	/// </summary>
	public class RequestHandler : IRequestHandler
	{
		/// <summary>
		/// Processes the OWIN HTTP request.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public Task ProcessRequest(IOwinContext context)
		{
			return Task.Delay(0);
		}
	}
}
