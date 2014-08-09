using System;

namespace AcspNet.DI
{
	/// <summary>
	/// Represents DI container provider
	/// </summary>
	public interface IDIContainerProvider
	{
		/// <summary>
		/// Resolves the specified type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		object Resolve(Type type);

		/// <summary>
		/// Registers the specified service type with corresponding implemetation type.
		/// </summary>
		/// <param name="serviceType">Service type.</param>
		/// <param name="implementationType">Implementation type.</param>
		/// <param name="lifetimeType">Lifetime type of the registering service type.</param>
		void Register(Type serviceType, Type implementationType, LifetimeType lifetimeType = LifetimeType.Transient);

		/// <summary>
		/// Registers the specified concrete type for resolve with delegate for concrete implementaion instance creation.
		/// </summary>
		/// <typeparam name="TConcrete">Concrete type.</typeparam>
		/// <param name="provider">The DI provider.</param>
		/// <param name="instanceCreator">The instance creator.</param>
		/// <param name="lifetimeType">Lifetime type of the registering concrete type.</param>
		void Register<TConcrete>(IDIContainerProvider provider, Func<TConcrete> instanceCreator,
			LifetimeType lifetimeType = LifetimeType.Transient)
			where TConcrete : class;

		/// <summary>
		/// Begins the lifetime scope.
		/// </summary>
		/// <returns></returns>
		ILifetimeScope BeginLifetimeScope();
	}
}