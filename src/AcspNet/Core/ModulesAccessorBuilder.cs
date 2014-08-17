using AcspNet.Modules;
using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Provides builder for ModulesAccessor objects
	/// </summary>
	public class ModulesAccessorBuilder
	{
		/// <summary>
		/// Builds the modules accessor properties.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="modulesAccessor">The modules accessor.</param>
		protected void BuildModulesAccessorProperties(IDIContainerProvider containerProvider, ModulesAccessor modulesAccessor)
		{
			modulesAccessor.Environment = containerProvider.Resolve<IEnvironment>();

			var stringTable = containerProvider.Resolve<IStringTable>();
			modulesAccessor.StringTable = stringTable.Items;
			modulesAccessor.StringTableManager = stringTable;

			modulesAccessor.TemplateFactory = containerProvider.Resolve<ITemplateFactory>();
		}
	}
}