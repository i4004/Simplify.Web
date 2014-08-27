using System.Threading.Tasks;
using Microsoft.Owin;

namespace AcspNet.Core
{
	/// <summary>
	/// Provides static files request handler
	/// </summary>
	public class StaticFilesRequestHandler : IStaticFilesRequestHandler
	{
		/// <summary>
		/// Determines whether current route path is for static file.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public bool IsStaticFileRoutePath(IOwinContext context)
		{
			return false;
		}

		/// <summary>
		/// Processes the HTTP request for static files.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public Task ProcessRequest(IOwinContext context)
		{
			return Task.Delay(0);
		}
	}
}