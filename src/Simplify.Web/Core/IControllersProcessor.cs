using Microsoft.Owin;
using Simplify.DI;

namespace Simplify.Web.Core
{
	/// <summary>
	/// Represents controllers processor
	/// </summary>
	public interface IControllersProcessor
	{
		/// <summary>
		/// Process controllers for current HTTP request
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		ControllersProcessorResult ProcessControllers(IDIContainerProvider containerProvider, IOwinContext context);
	}
}