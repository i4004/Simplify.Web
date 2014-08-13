using System;
using AcspNet.DI;
using Microsoft.Practices.Unity;

namespace AcspNet.DIContainerProvider.Unity
{
	/// <summary>
	/// Unity container provider implementation
	/// </summary>
	public class UnityDIProvider : IDIContainerProvider
	{
		private IUnityContainer _container;

		/// <summary>
		/// The IOC container
		/// </summary>
		public IUnityContainer Container
		{
			get
			{
				return _container ?? (_container = new UnityContainer());
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				_container = value;
			}
		}

		/// <summary>
		/// Resolves the specified type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public object Resolve(Type type)
		{
			return Container.Resolve(type);
		}

		/// <summary>
		/// Registers the specified service type with corresponding implementation type.
		/// </summary>
		/// <param name="serviceType">Service type.</param>
		/// <param name="implementationType">Implementation type.</param>
		/// <param name="lifetimeType">Lifetime type of the registering service type.</param>
		public void Register(Type serviceType, Type implementationType, LifetimeType lifetimeType = LifetimeType.Singleton)
		{
			switch (lifetimeType)
			{
				case LifetimeType.Transient:
					Container.RegisterType(serviceType, implementationType, new TransientLifetimeManager());
					break;

				case LifetimeType.Singleton:
					Container.RegisterType(serviceType, implementationType, new ContainerControlledLifetimeManager());
					break;

				case LifetimeType.PerLifetimeScope:
					Container.RegisterType(serviceType, implementationType, new PerThreadLifetimeManager());
					break;
			}
		}

		/// <summary>
		/// Registers the specified instance creator.
		/// </summary>
		/// <typeparam name="TService">The type of the service.</typeparam>
		/// <param name="instanceCreator">The instance creator.</param>
		/// <param name="lifetimeType">Type of the lifetime.</param>
		public void Register<TService>(Func<IDIContainerProvider, TService> instanceCreator, LifetimeType lifetimeType = LifetimeType.Singleton)
			where TService : class
		{
			switch (lifetimeType)
			{
				case LifetimeType.Transient:
					Container.RegisterType<TService>(new TransientLifetimeManager(), new InjectionFactory(container =>
					{
						var provider = new UnityDIProvider { Container = container };
						return instanceCreator(provider);
					}));
					break;

				case LifetimeType.Singleton:
					Container.RegisterType<TService>(new ContainerControlledLifetimeManager(), new InjectionFactory(container =>
					{
						var provider = new UnityDIProvider { Container = container };
						return instanceCreator(provider);
					}));
					break;

				case LifetimeType.PerLifetimeScope:
					Container.RegisterType<TService>(new PerThreadLifetimeManager(), new InjectionFactory(container =>
					{
						var provider = new UnityDIProvider { Container = container };
						return instanceCreator(provider);
					}));
					break;
			}
		}

		/// <summary>
		/// Begins the lifetime scope.
		/// </summary>
		/// <returns></returns>
		public ILifetimeScope BeginLifetimeScope()
		{
			return new UnityLifetimeScope(this);
		}
	}
}