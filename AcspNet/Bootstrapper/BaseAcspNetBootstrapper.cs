using System;
using AcspNet.Meta;
using AcspNet.Routing;
using DryIoc;

namespace AcspNet.Bootstrapper
{
	/// <summary>
	/// Base and default AcspNet bootstrapper
	/// </summary>
	public class BaseAcspNetBootstrapper
	{
		private Type _controllerFactoryType;
		private Type _controllerMetaDataFactoryType;
		private Type _controllersMetaStoreType;
		private Type _routeMatcherType;
		private Type _controllersAgentType;
		private Type _controllersHanderType;
		private Type _requestHandlerType;

		/// <summary>
		/// Registers the types in container.
		/// </summary>
		public void Register()
		{
			// Registering AcspNet types

			DependencyResolver.Container.Register(typeof(IControllerFactory), ControllerFactoryType, Reuse.Singleton);
			DependencyResolver.Container.Register(typeof(IControllerMetaDataFactory), ControllerMetaDataFactoryType, Reuse.Singleton);
			DependencyResolver.Container.Register(typeof(IControllersMetaStore), ControllersMetaStoreType, Reuse.Singleton);
			DependencyResolver.Container.Register(typeof(IRouteMatcher), RouteMatcherType, Reuse.Singleton);
			DependencyResolver.Container.Register(typeof(IControllersAgent), ControllersAgentType, Reuse.Singleton);
			DependencyResolver.Container.Register(typeof(IControllersHandler), ControllersHandlerType, Reuse.Singleton);
			DependencyResolver.Container.Register(typeof(IRequestHandler), RequestHandlerType, Reuse.Singleton);

			// Registering controllers

			foreach (var controllerMetaData in DependencyResolver.Container.Resolve<IControllersMetaStore>().ControllersMetaData)
				DependencyResolver.Container.Register(controllerMetaData.ControllerType);
		}

		/// <summary>
		/// Gets the type of the controller factory.
		/// </summary>
		/// <value>
		/// The type of the controller factory.
		/// </value>
		public Type ControllerFactoryType
		{
			get { return _controllerFactoryType ?? typeof(ControllerFactory); }
		}

		/// <summary>
		/// Gets the type of the controller meta data factory.
		/// </summary>
		/// <value>
		/// The type of the controller meta data factory.
		/// </value>
		public Type ControllerMetaDataFactoryType
		{
			get { return _controllerMetaDataFactoryType ?? typeof(ControllerMetaDataFactory); }
		}

		/// <summary>
		/// Gets the type of the controllers meta store.
		/// </summary>
		/// <value>
		/// The type of the controllers meta store.
		/// </value>
		public Type ControllersMetaStoreType
		{
			get { return _controllersMetaStoreType ?? typeof(ControllersMetaStore); }
		}

		/// <summary>
		/// Gets the type of the route matcher.
		/// </summary>
		/// <value>
		/// The type of the route matcher.
		/// </value>
		public Type RouteMatcherType
		{
			get { return _routeMatcherType ?? typeof(RouteMatcher); }
		}

		/// <summary>
		/// Gets the type of the controllers agent.
		/// </summary>
		/// <value>
		/// The type of the controllers agent.
		/// </value>
		public Type ControllersAgentType
		{
			get { return _controllersAgentType ?? typeof(ControllersAgent); }
		}

		/// <summary>
		/// Gets the type of the controllers handler.
		/// </summary>
		/// <value>
		/// The type of the controllers handler.
		/// </value>
		public Type ControllersHandlerType
		{
			get { return _controllersHanderType ?? typeof(ControllersHandler); }
		}

		/// <summary>
		/// Gets the type of the request handler.
		/// </summary>
		/// <value>
		/// The type of the request handler.
		/// </value>
		public Type RequestHandlerType
		{
			get { return _requestHandlerType ?? typeof(RequestHandler); }
		}

		/// <summary>
		/// Sets the type of the controller factory.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public void SetControllerFactoryType<T>()
			where T : IControllerFactory
		{
			_controllerFactoryType = typeof(T);
		}

		/// <summary>
		/// Sets the type of the controller meta data factory.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public void SetControllerMetaDataFactoryType<T>()
			where T : IControllerMetaDataFactory
		{
			_controllerMetaDataFactoryType = typeof(T);
		}

		/// <summary>
		/// Sets the type of the controllers meta store.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public void SetControllersMetaStoreType<T>()
			where T : IControllersMetaStore
		{
			_controllersMetaStoreType = typeof(T);
		}

		/// <summary>
		/// Sets the type of the route matcher.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public void SetRouteMatcherType<T>()
			where T : IRouteMatcher
		{
			_routeMatcherType = typeof(T);
		}

		/// <summary>
		/// Sets the type of the controllers agent.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public void SetControllersAgentType<T>()
			where T : IControllersAgent
		{
			_controllersAgentType = typeof(T);
		}

		/// <summary>
		/// Sets the type of the controllers handler.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public void SetControllersHandlerType<T>()
			where T : IControllersHandler
		{
			_controllersHanderType = typeof(T);
		}

		/// <summary>
		/// Sets the type of the request handler.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public void SetRequestHandlerType<T>()
			where T : IRequestHandler
		{
			_requestHandlerType = typeof(T);
		}
	}
}