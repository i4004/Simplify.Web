using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Represent controller response builder
	/// </summary>
	public interface IControllerResponseBuilder
	{
		/// <summary>
		/// Builds the controller response properties.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="controllerResponse">The controller response.</param>
		void BuildControllerResponseProperties(IDIContainerProvider containerProvider, ControllerResponse controllerResponse);
	}
}