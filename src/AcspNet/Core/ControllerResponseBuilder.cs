using System;
using AcspNet.Modules;
using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Provides controller response builder
	/// </summary>
	public class ControllerResponseBuilder : IControllerResponseBuilder
	{
		/// <summary>
		/// Builds the controller response properties.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="controllerResponse">The controller response.</param>
		public void BuildControllerResponseProperties(IDIContainerProvider containerProvider, ControllerResponse controllerResponse)
		{
			controllerResponse.DataCollector = containerProvider.Resolve<IDataCollector>();
		}
	}
}