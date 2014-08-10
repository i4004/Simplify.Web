using System.Threading.Tasks;
using AcspNet.DI;
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
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		Task ProcessRequest(IDIContainerProvider containerProvider, IOwinContext context);
	}
}