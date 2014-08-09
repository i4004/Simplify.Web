using System;

namespace AcspNet.DI
{
	/// <summary>
	/// Provides DI container provider register extensions
	/// </summary>
	public static class DIContainerProviderRegisterExtensions
	{
		/// <summary>
		/// Registers the specified concrete type for resolve.
		/// </summary>
		/// <param name="provider">The DI provider.</param>
		/// <param name="concreteType">Concrete type.</param>
		/// <param name="lifetimeType">Lifetime type of the registering concrete type.</param>
		public static void Register(IDIContainerProvider provider, Type concreteType, LifetimeType lifetimeType = LifetimeType.Transient)
		{
			provider.Register(concreteType, concreteType, lifetimeType);
		}

		/// <summary>
		/// Registers the specified service type with corresponding implemetation type.
		/// </summary>
		/// <typeparam name="TService">Service type.</typeparam>
		/// <typeparam name="TImplementation">Implementation type.</typeparam>
		/// <param name="provider">The DI provider.</param>
		/// <param name="lifetimeType">Lifetime type of the registering service type.</param>
		public static void Register<TService, TImplementation>(IDIContainerProvider provider, LifetimeType lifetimeType = LifetimeType.Transient)
		{
			provider.Register(typeof(TService), typeof(TImplementation), lifetimeType);
		}

		/// <summary>
		/// Registers the specified concrete type for resolve.
		/// </summary>
		/// <typeparam name="TConcrete">Concrete type.</typeparam>
		/// <param name="provider">The DI provider.</param>
		/// <param name="lifetimeType">Lifetime type of the registering concrete type.</param>
		public static void Register<TConcrete>(IDIContainerProvider provider, LifetimeType lifetimeType = LifetimeType.Transient)
		{
			provider.Register(typeof(TConcrete), typeof(TConcrete), lifetimeType);
		}

		/// <summary>
		/// Registers the specified concrete type for resolve with delegate for concrete implementaion instance creation.
		/// </summary>
		/// <typeparam name="TConcrete">Concrete type.</typeparam>
		/// <param name="provider">The DI provider.</param>
		/// <param name="instanceCreator">The instance creator.</param>
		/// <param name="lifetimeType">Lifetime type of the registering concrete type.</param>
		public static void Register<TConcrete>(IDIContainerProvider provider, Func<object> instanceCreator, LifetimeType lifetimeType = LifetimeType.Transient)
		{
			provider.Register(typeof(TConcrete), instanceCreator, lifetimeType);
		}
	}
}