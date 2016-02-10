using System.Threading.Tasks;

namespace Simplify.Web.Core
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