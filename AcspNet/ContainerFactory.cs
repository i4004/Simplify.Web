using System;

namespace AcspNet
{
	public class ContainerFactory : IContainerFactory
	{
		private static IDependecyResolver _dependencyResolver;
		private readonly ModulesContainer _modulesContainer;

		internal ContainerFactory(ModulesContainer modulesContainer)
		{
			_modulesContainer = modulesContainer;
		}
		
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

		public Controller CreateController(Type controllerType)
		{
			var controller = (Controller)DependencyResolver.Resolve(controllerType);

			FillContainer(controller);

			return controller;
		}

		private void FillContainer(Container container)
		{
			container.FileReader = _modulesContainer.FileReader;
		}
	}
}