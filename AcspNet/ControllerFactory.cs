using System;

namespace AcspNet
{
	/// <summary>
	/// Controller factory
	/// </summary>
	public class ControllerFactory : IControllerFactory
	{
		private readonly ModulesContainer _sourceContainer;
		private readonly IViewFactory _viewFactory;
		
		internal ControllerFactory(ModulesContainer sourceContainer, IViewFactory viewFactory)
		{
			_sourceContainer = sourceContainer;
			_viewFactory = viewFactory;
		}

		/// <summary>
		/// Creates the controller.
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <returns></returns>
		public Controller CreateController(Type controllerType)
		{
			var controller = (Controller)DependencyResolver.Current.Resolve(controllerType);

			controller.Context = _sourceContainer.Context;
			controller.Environment = _sourceContainer.Environment;
			controller.LanguageManager = _sourceContainer.LanguageManager;
			controller.FileReader = _sourceContainer.FileReader;
			controller.DataCollector = _sourceContainer.DataCollector;
			controller.TemplateFactory = _sourceContainer.TemplateFactory;
			controller.StringTable = _sourceContainer.StringTable;
			controller.MessageBox = _sourceContainer.MessageBox;
			controller.Authentication = _sourceContainer.Authentication;
			controller.Navigator = _sourceContainer.Navigator;
			controller.IdVerifier = _sourceContainer.IdVerifier;
			controller.ViewFactory = _viewFactory;
			controller.MessagePage = _sourceContainer.MessagePage;

			return controller;
		}
	}
}