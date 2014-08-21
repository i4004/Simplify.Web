using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;
using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Provides controllers processor, handles creation and execution of controllers
	/// </summary>
	public class ControllersProcessor : IControllersProcessor
	{
		private readonly IControllerFactory _controllerFactory;
		private readonly IControllerResponseBuilder _controllerResponseBuilder;

		private readonly IList<Task<ControllerResponse>> _controllersResponses = new List<Task<ControllerResponse>>();

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllersProcessor"/> class.
		/// </summary>
		/// <param name="controllerFactory">The controller factory.</param>
		/// <param name="controllerResponseBuilder">The controller response builder.</param>
		public ControllersProcessor(IControllerFactory controllerFactory, IControllerResponseBuilder controllerResponseBuilder)
		{
			_controllerFactory = controllerFactory;
			_controllerResponseBuilder = controllerResponseBuilder;
		}

		/// <summary>
		/// Processes the specified controller (creates and executes controller).
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <param name="containerProvider">The container provider.</param>
		/// <param name="context">The context.</param>
		/// <param name="routeParameters">The route parameters.</param>
		/// <returns></returns>
		public ControllerResponseResult Process(Type controllerType, IDIContainerProvider containerProvider, IOwinContext context,
			dynamic routeParameters = null)
		{
			var controller = _controllerFactory.CreateController(controllerType, containerProvider, context, routeParameters);

			var synController = controller as Controller;

			if (synController != null)
			{
				var response = synController.Invoke();

				return ProcessControllerResponse(response, containerProvider);
			}

			var asyncController = controller as AsyncController;

			if (asyncController != null)
			{
				var task = asyncController.Invoke();
				_controllersResponses.Add(task);
			}

			return ControllerResponseResult.Default;
		}

		/// <summary>
		/// Processes the asynchronous controllers responses.
		/// </summary>
		/// <param name="containerProvider">The container provider.</param>
		/// <returns></returns>
		public IEnumerable<ControllerResponseResult> ProcessAsyncControllersResponses(IDIContainerProvider containerProvider)
		{
			foreach (var task in _controllersResponses)
			{
				task.Wait();
				yield return ProcessControllerResponse(task.Result, containerProvider);
			}
		}

		private ControllerResponseResult ProcessControllerResponse(ControllerResponse response, IDIContainerProvider containerProvider)
		{
			if (response == null)
				return ControllerResponseResult.Default;

			_controllerResponseBuilder.BuildControllerResponseProperties(response, containerProvider);

			return response.Process();
		}
	}
}