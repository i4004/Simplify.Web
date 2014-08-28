using AcspNet.Modules;
using AcspNet.Modules.Html;
using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Provides controller response builder
	/// </summary>
	public class ControllerResponseBuilder : ModulesAccessorBuilder, IControllerResponseBuilder
	{
		/// <summary>
		/// Builds the controller response properties.
		/// </summary>
		/// <param name="controllerResponse">The controller response.</param>
		/// <param name="containerProvider">The DI container provider.</param>
		public void BuildControllerResponseProperties(ControllerResponse controllerResponse, IDIContainerProvider containerProvider)
		{
			BuildModulesAccessorProperties(controllerResponse, containerProvider);

			controllerResponse.Context = containerProvider.Resolve<IAcspNetContext>();
			controllerResponse.DataCollector = containerProvider.Resolve<IDataCollector>();
			controllerResponse.Redirector = containerProvider.Resolve<IRedirector>();
			controllerResponse.ResponseWriter = containerProvider.Resolve<IResponseWriter>();

			var htmlWrapper = new HtmlWrapper {MessageBox = containerProvider.Resolve<IMessageBox>()};
			controllerResponse.Html = htmlWrapper;
		}
	}
}