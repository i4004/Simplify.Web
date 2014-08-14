using AcspNet.Modules;
using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Provides factory for ModulesAccessor objects construction
	/// </summary>
	public class ModulesAccessorFactory : ViewAccessorFactory
	{
		/// <summary>
		/// Constructs the modules accessor.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="modulesAccessor">The modules accessor.</param>
		protected void ConstructModulesAccessor(IDIContainerProvider containerProvider, ModulesAccessor modulesAccessor)
		{
			modulesAccessor.Environment = containerProvider.Resolve<IEnvironment>();		
		}
	}
}