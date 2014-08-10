using System;
using AcspNet.DI;
using Castle.MicroKernel.Lifestyle;

namespace AcspNet.DIContainerProvider.CastleWindsor
{
	/// <summary>
	/// Castle Windsor DI provider lifetime scope implementation
	/// </summary>
	public class CastleWindsorLifetimeScope : ILifetimeScope
	{
		private readonly IDisposable _scope;

		/// <summary>
		/// Initializes a new instance of the <see cref="CastleWindsorLifetimeScope"/> class.
		/// </summary>
		/// <param name="provider">The provider.</param>
		public CastleWindsorLifetimeScope(CastleWindsorDIProvider provider)
		{
			Container = provider;

			_scope = provider.Container.BeginScope();
		}

		/// <summary>
		/// Gets the DI container provider (shoud be user to resolve types when using scoping).
		/// </summary>
		/// <value>
		/// The DI container provider (shoud be user to resolve types when using scoping).
		/// </value>
		public IDIContainerProvider Container { get; private set; }

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			_scope.Dispose();
		}
	}
}