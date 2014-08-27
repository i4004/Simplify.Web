using System.Threading.Tasks;
using Microsoft.Owin;
using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Represent controllers HTTP request handler
	/// </summary>
	public interface IControllersRequestHandler
	{
		/// <summary>
		/// Processes the HTTP request for controllers.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		Task ProcessRequest(IDIContainerProvider containerProvider, IOwinContext context);
	}
}