using System.Threading.Tasks;

namespace Simplify.Web.Core
{
	/// <summary>
	/// Represent OWIN HTTP request handler
	/// </summary>
	public interface IRequestHandler
	{
		/// <summary>
		/// Processes the OWIN HTTP request.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		Task ProcessRequest(IDIContainerProvider containerProvider, IOwinContext context);
	}
}