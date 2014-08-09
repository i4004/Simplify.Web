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
		/// Registers the specified service type with delegate for service implementaion instance creation.
		/// </summary>
		/// <param name="serviceType">Service type.</param>
		/// <param name="instanceCreator">The instance creator.</param>
		/// <param name="lifetimeType">Lifetime type of the registering service type</param>
		void Register(Type serviceType, Func<object> instanceCreator, LifetimeType lifetimeType = LifetimeType.Transient);

		/// <summary>
		/// Begins the lifetime scope.
		/// </summary>
		/// <returns></returns>
		ILifetimeScope BeginLifetimeScope();
	}
}