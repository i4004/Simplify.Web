using System;

namespace AcspNet
{
	/// <summary>
	/// Controller factory
	/// </summary>
	public class ControllerFactory : ContainerFactory, IControllerFactory
	{
		private readonly IViewFactory _viewFactory;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerFactory"/> class.
		/// </summary>
		/// <param name="sourceContainer">The source container.</param>
		/// <param name="viewFactory">The view factory.</param>
		internal ControllerFactory(SourceContainer sourceContainer, IViewFactory viewFactory) : base(sourceContainer)
		{
			_viewFactory = viewFactory;
		}

		/// <summary>
		/// Creates the controller.
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <returns></returns>
		public Controller CreateController(Type controllerType)
		{
			var controller = (Controller)base.CreateContainer(controllerType);

			controller.ViewFactory = _viewFactory;

			return controller;
		}
	}
}