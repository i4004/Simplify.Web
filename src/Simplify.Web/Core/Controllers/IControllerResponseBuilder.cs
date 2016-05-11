using Simplify.DI;

namespace Simplify.Web.Core.Controllers
{
	/// <summary>
	/// Represent controller response builder
	/// </summary>
	public interface IControllerResponseBuilder
	{
		/// <summary>
		/// Builds the controller response properties.
		/// </summary>
		/// <param name="controllerResponse">The controller response.</param>
		/// <param name="containerProvider">The DI container provider.</param>
		void BuildControllerResponseProperties(ControllerResponse controllerResponse, IDIContainerProvider containerProvider);
	}
}