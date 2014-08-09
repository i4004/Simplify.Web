using System;
using AcspNet.DryIoc;

namespace AcspNet.DI.DIContainerProvider.DryIoc
{
	/// <summary>
	/// DryIoc DI container provider implementation
	/// </summary>
	public class DryIocDIProvider : IDIContainerProvider
	{
		private Container _container;

		/// <summary>
		/// The IOC container
		/// </summary>
		public Container Container
		{
			get
			{
				return _container ?? (_container = new Container());
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				_container = value;
			}
		}

		/// <summary>
		/// Resolves the specified service type.
		/// </summary>
		/// <param name="serviceType">Type of the service.</param>
		/// <returns></returns>
		public object Resolve(Type serviceType)
		{
			return Container.Resolve(serviceType);
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
					Container.Register(serviceType, implementationType, Reuse.Transient);
					break;

				case LifetimeType.Singleton:
					Container.Register(serviceType, implementationType, Reuse.Singleton);
					break;

				case LifetimeType.PerLifetimeScope:
					Container.Register(serviceType, implementationType, Reuse.InCurrentScope);
					break;
			}
		}

		/// <summary>
		/// Registers the specified concrete type for resolve with delegate for concrete implementaion instance creation.
		/// </summary>
		/// <typeparam name="TConcrete">Concrete type.</typeparam>
		/// <param name="instanceCreator">The instance creator.</param>
		/// <param name="lifetimeType">Lifetime type of the registering concrete type.</param>
		public void Register<TConcrete>(Func<TConcrete> instanceCreator,
			LifetimeType lifetimeType = LifetimeType.Transient)
			where TConcrete : class
		{
			switch (lifetimeType)
			{
				case LifetimeType.Transient:
					Container.RegisterDelegate(_ => instanceCreator(), Reuse.Transient);
					break;

				case LifetimeType.Singleton:
					Container.RegisterDelegate(_ => instanceCreator(), Reuse.Singleton);
					break;

				case LifetimeType.PerLifetimeScope:
					Container.RegisterDelegate(_ => instanceCreator(), Reuse.InCurrentScope);
					break;
			}
		}

		/// <summary>
		/// Begins the lifetime scope.
		/// </summary>
		/// <returns></returns>
		public ILifetimeScope BeginLifetimeScope()
		{
			return new DryIocLifetimeScope(this);
		}
	}
}