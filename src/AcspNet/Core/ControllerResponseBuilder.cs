namespace Simplify.Web.Core
{
	/// <summary>
	/// Provides controller response builder
	/// </summary>
	public class ControllerResponseBuilder : ActionModulesAccessorBuilder, IControllerResponseBuilder
	{
		/// <summary>
		/// Builds the controller response properties.
		/// </summary>
		/// <param name="controllerResponse">The controller response.</param>
		/// <param name="containerProvider">The DI container provider.</param>
		public void BuildControllerResponseProperties(ControllerResponse controllerResponse, IDIContainerProvider containerProvider)
		{
			BuildActionModulesAccessorProperties(controllerResponse, containerProvider);

			controllerResponse.ResponseWriter = containerProvider.Resolve<IResponseWriter>();
		}
	}
}