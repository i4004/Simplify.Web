using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Owin;
using Simplify.DI;

namespace Simplify.Web.Core
{
	/// <summary>
	/// Provides controller executor, handles creation and execution of controllers
	/// </summary>
	public class ControllerExecutor : IControllerExecutor
	{
		private readonly IControllerFactory _controllerFactory;
		private readonly IControllerResponseBuilder _controllerResponseBuilder;

		private readonly IList<Task<ControllerResponse>> _controllersResponses = new List<Task<ControllerResponse>>();

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerExecutor"/> class.
		/// </summary>
		/// <param name="controllerFactory">The controller factory.</param>
		/// <param name="controllerResponseBuilder">The controller response builder.</param>
		public ControllerExecutor(IControllerFactory controllerFactory, IControllerResponseBuilder controllerResponseBuilder)
		{
			_controllerFactory = controllerFactory;
			_controllerResponseBuilder = controllerResponseBuilder;
		}
		
		/// <summary>
		/// Creates and executes the specified controller.
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <param name="containerProvider">The container provider.</param>
		/// <param name="context">The context.</param>
		/// <param name="routeParameters">The route parameters.</param>
		/// <returns></returns>
		public ControllerResponseResult Execute(Type controllerType, IDIContainerProvider containerProvider, IOwinContext context,
			dynamic routeParameters = null)
		{
			ControllerBase controller = _controllerFactory.CreateController(controllerType, containerProvider, context, routeParameters);

			var syncController = controller as SyncControllerBase;

			if (syncController != null)
				return ProcessControllerResponse(syncController.Invoke(), containerProvider);

			var asyncController = controller as AsyncControllerBase;

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