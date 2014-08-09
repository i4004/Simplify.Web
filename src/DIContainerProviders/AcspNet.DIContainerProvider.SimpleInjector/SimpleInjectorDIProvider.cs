using System;
using AcspNet.DI;
using SimpleInjector;
using SimpleInjector.Extensions.LifetimeScoping;

namespace AcspNet.DIContainerProvider.SimpleInjector
{
	/// <summary>
	/// Simple Injector DI container provider implementation
	/// </summary>
	public class SimpleInjectorDIProvider : IDIContainerProvider
	{
		/// <summary>
		/// The IOC container
		/// </summary>
		public Container Container = new Container();

		/// <summary>
		/// Resolves the specified service type.
		/// </summary>
		/// <param name="serviceType">Type of the service.</param>
		/// <returns></returns>
		public object Resolve(Type serviceType)
		{
			return Container.GetInstance(serviceType);
		}

		/// <summary>
		/// Registers the specified service type with corresponding implemetation type.
		/// </summary>
		/// <param name="serviceType">Service type.</param>
		/// <param name="implementationType">Implementation type.</param>
		/// <param name="lifetimeType">Lifetime type of the registering services type.</param>
		public void Register(Type serviceType, Type implementationType, LifetimeType lifetimeType = LifetimeType.Transient)
		{
			switch (lifetimeType)
			{
				case LifetimeType.Transient:
					Container.Register(serviceType, implementationType, Lifestyle.Transient);
					break;

				case LifetimeType.Singleton:
					Container.Register(serviceType, implementationType, Lifestyle.Singleton);
					break;

				case LifetimeType.PerLifetimeScope:
					Container.Register(serviceType, implementationType, new LifetimeScopeLifestyle());
					break;
			}
		}

		/// <summary>
		/// Registers the specified service type with delegate for service implementaion instance creation.
		/// </summary>
		/// <param name="serviceType">Service.</param>
		/// <param name="instanceCreator">The instance creator.</param>
		/// <param name="lifetimeType">Lifetime type of the registering service type</param>
		public void Register(Type serviceType, Func<object> instanceCreator, LifetimeType lifetimeType = LifetimeType.Transient)
		{
			switch (lifetimeType)
			{
				case LifetimeType.Transient:
					Container.Register(serviceType, instanceCreator, Lifestyle.Transient);
					break;

				case LifetimeType.Singleton:
					Container.Register(serviceType, instanceCreator, Lifestyle.Singleton);
					break;

				case LifetimeType.PerLifetimeScope:
					Container.Register(serviceType, instanceCreator, new LifetimeScopeLifestyle());
					break;
			}
		}

		/// <summary>
		/// Begins the lifetime scope.
		/// </summary>
		/// <returns></returns>
		public ILifetimeScope BeginLifetimeScope()
		{
			return new SimpleInjectorLifetimeScope(this);
		}
	}
}