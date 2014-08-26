using Microsoft.Owin;
using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Represent web-page processor
	/// </summary>
	public interface IPageProcessor
	{
		/// <summary>
		/// Processes (build web-page and send to client, process current page state) the current web-page
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="context">The context.</param>
		void ProcessPage(IDIContainerProvider containerProvider, IOwinContext context);
	}
}