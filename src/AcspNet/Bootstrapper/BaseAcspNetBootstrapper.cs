using System;
using AcspNet.Core;
using AcspNet.DI;
using AcspNet.Meta;
using AcspNet.Modules;
using AcspNet.Routing;
using Environment = AcspNet.Modules.Environment;

namespace AcspNet.Bootstrapper
{
	/// <summary>
	/// Base and default AcspNet bootstrapper
	/// </summary>
	public class BaseAcspNetBootstrapper
	{
		private Type _acspNetSettingsType;
		private Type _controllerFactoryType;
		private Type _routeMatcherType;
		private Type _controllersAgentType;
		private Type _controllersHanderType;
		private Type _requestHandlerType;

		//private Type _environmentType;

		/// <summary>
		/// Registers the types in container.
		/// </summary>
		public void Register()
		{
			// Registering AcspNet core types

			RegisterControllersMetaStore();
			RegisterAcspNetSettings();
			RegisterControllerFactory();
			RegisterRouteMatcher();
			RegisterControllersAgent();
			RegisterControllersHandler();
			RegisterRequestHandler();

			// Registeting user modules

			RegisterEnvironment();

			// Registering controllers types
			//foreach (var controllerMetaData in ControllersMetaStore.Current.ControllersMetaData)
			//	DIContainer.Current.Register(controllerMetaData.ControllerType);
		}

		#region Bootstrapper types

		/// <summary>
		/// Gets the type of the AcspNet settings.
		/// </summary>
		/// <value>
		/// The type of the AcspNet settings.
		/// </value>
		public Type AcspNetSettingsType
		{
			get { return _acspNetSettingsType ?? typeof(AcspNetSettings); }
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

		///// <summary>
		///// Gets the type of the environment.
		///// </summary>
		///// <value>
		///// The type of the environment.
		///// </value>
		//public Type EnvironmentType
		//{
		//	get { return _environmentType ?? typeof(Environment); }
		//}

		#endregion

		#region Bootstrapper types override

		/// <summary>
		/// Sets the type of the AcspNet settings.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public void SetAcspNetSettingsType<T>()
			where T : IAcspNetSettings
		{
			_acspNetSettingsType = typeof(T);
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

		///// <summary>
		///// Sets the type of the environment.
		///// </summary>
		///// <typeparam name="T"></typeparam>
		//public void SetEnvironmentType<T>()
		//	where T : IEnvironment
		//{
		//	_environmentType = typeof(T);
		//}

		#endregion

		#region Bootstrapper types registration

		public virtual void RegisterControllersMetaStore()
		{
			DIContainer.Current.Register(() => ControllersMetaStore.Current, LifetimeType.Singleton);
		}

		public virtual void RegisterAcspNetSettings()
		{
			DIContainer.Current.Register<IAcspNetSettings>(AcspNetSettingsType, LifetimeType.Singleton);
		}

		public virtual void RegisterControllerFactory()
		{
			DIContainer.Current.Register<IControllerFactory>(ControllerFactoryType, LifetimeType.Singleton);
		}

		public virtual void RegisterRouteMatcher()
		{
			DIContainer.Current.Register<IRouteMatcher>(RouteMatcherType, LifetimeType.Singleton);
		}

		public virtual void RegisterControllersAgent()
		{
			DIContainer.Current.Register<IControllersAgent>(ControllersAgentType, LifetimeType.Singleton);
		}

		public virtual void RegisterControllersHandler()
		{
			DIContainer.Current.Register<IControllersHandler>(ControllersHandlerType, LifetimeType.Singleton);
		}

		public virtual void RegisterRequestHandler()
		{
			DIContainer.Current.Register<IRequestHandler>(RequestHandlerType, LifetimeType.Singleton);
		}

		public virtual void RegisterEnvironment()
		{
			// todo pass scope container to delegate
			DIContainer.Current.Register<IEnvironment>(
				() => new Environment(AppDomain.CurrentDomain.BaseDirectory, DIContainer.Current.Resolve<IAcspNetSettings>()),
				LifetimeType.PerLifetimeScope);
		}

		#endregion
	}
}