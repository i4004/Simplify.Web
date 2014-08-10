using AcspNet.DI;

namespace AcspNet.DIContainerProvider.Unity
{
	/// <summary>
	/// Unity DI provider lifetime scope implementation
	/// </summary>
	public class UnityLifetimeScope : ILifetimeScope
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UnityLifetimeScope"/> class.
		/// </summary>
		/// <param name="provider">The provider.</param>
		public UnityLifetimeScope(IDIContainerProvider provider)
		{
			Container = provider;
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
		}
	}
}