using System;

namespace AcspNet
{
	/// <summary>
	/// Controlles and views creation factory
	/// </summary>
	public class ContainerFactory
	{
		private static IDependecyResolver _dependencyResolver;
		private readonly SourceContainer _sourceContainer;

		internal ContainerFactory(SourceContainer sourceContainer)
		{
			_sourceContainer = sourceContainer;
		}

		/// <summary>
		/// Gets or sets the dependency resolver fro container factory.
		/// </summary>
		/// <value>
		/// The dependency resolver.
		/// </value>
		/// <exception cref="System.ArgumentNullException">value</exception>
		public static IDependecyResolver DependencyResolver
		{
			get { return _dependencyResolver ?? (_dependencyResolver = new DefaultDependencyResolver()); }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				_dependencyResolver = value;
			}
		}

		/// <summary>
		/// Creates the controller or views derived from container class.
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <returns></returns>
		protected virtual Container CreateContainer(Type controllerType)
		{
			var controller = (Container)DependencyResolver.Resolve(controllerType);

			FillContainer(controller);

			return controller;
		}

		/// <summary>
		/// Fills container modules properties with actual modules instances.
		/// </summary>
		/// <param name="container">The container.</param>
		private void FillContainer(Container container)
		{
			container.Context = _sourceContainer.Context;
			container.Environment = _sourceContainer.Environment;
			container.LanguageManager = _sourceContainer.LanguageManager;
			container.FileReader = _sourceContainer.FileReader;
			container.TemplateFactory = _sourceContainer.TemplateFactory;
			container.StringTable = _sourceContainer.StringTable;
			container.DataCollector = _sourceContainer.DataCollector;
			container.Html = _sourceContainer.Html;
			container.Authentication = _sourceContainer.Authentication;
			container.Navigator = _sourceContainer.Navigator;
			container.IdVerifier = _sourceContainer.IdVerifier;
		}
	}
}