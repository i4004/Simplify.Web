using AcspNet.Modules;

namespace AcspNet
{
	/// <summary>
	/// Modules accessor base class
	/// </summary>
	public abstract class ModulesAccessor
	{
		/// <summary>
		/// Current request environment data.
		/// </summary>
		public virtual IEnvironment Environment { get; internal set; }

		/// <summary>
		/// Gets the string table.
		/// </summary>
		/// <value>
		/// The string table.
		/// </value>
		public virtual dynamic StringTable { get; internal set; }

		/// <summary>
		/// Gets the string table manager.
		/// </summary>
		/// <value>
		/// The string table manager.
		/// </value>
		public virtual IStringTable StringTableManager { get; internal set; }

		/// <summary>
		/// Text templates loader.
		/// </summary>
		public virtual ITemplateFactory TemplateFactory { get; internal set; }
	}
}