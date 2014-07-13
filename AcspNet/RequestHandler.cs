using System.Threading.Tasks;
using Microsoft.Owin;

namespace AcspNet
{
	/// <summary>
	/// Provides OWIN HTTP request handler
	/// </summary>
	public class RequestHandler : IRequestHandler
	{
		private readonly IControllersHandler _controllersHandler;

		/// <summary>
		/// Initializes a new instance of the <see cref="RequestHandler"/> class.
		/// </summary>
		/// <param name="controllersHandler">The controllers handler.</param>
		public RequestHandler(IControllersHandler controllersHandler)
		{
			_controllersHandler = controllersHandler;
		}

		/// <summary>
		/// Processes the OWIN HTTP request.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public Task ProcessRequest(IOwinContext context)
		{
			_controllersHandler.Execute(context.Request.Path.Value);

			return Task.Delay(0);
		}
	}
}
