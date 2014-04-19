using System;

namespace AcspNet
{
	/// <summary>
	/// Controlles and views creation factory
	/// </summary>
	public class ContainerFactory : IContainerFactory
	{
		private static IDependecyResolver _dependencyResolver;
		private readonly SourceContainer _modulesContainer;

		internal ContainerFactory(SourceContainer modulesContainer)
		{
			_modulesContainer = modulesContainer;
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
		public Container CreateContainer(Type controllerType)
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
			container.Context = _modulesContainer.Context;
			container.Environment = _modulesContainer.Environment;
			container.LanguageManager = _modulesContainer.LanguageManager;
			container.FileReader = _modulesContainer.FileReader;
			container.TemplateFactory = _modulesContainer.TemplateFactory;
			container.StringTable = _modulesContainer.StringTable;
			container.DataCollector = _modulesContainer.DataCollector;

			container.ContainerFactory = this;
		}
	}
}