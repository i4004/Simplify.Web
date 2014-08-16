using AcspNet.Modules;

namespace AcspNet
{
	/// <summary>
	/// Modules accessor base class
	/// </summary>
	public abstract class ModulesAccessor : ViewAccessor
	{
		/// <summary>
		/// Current request environment data.
		/// </summary>
		public virtual IEnvironment Environment { get; internal set; }

		/// <summary>
		/// Text templates loader.
		/// </summary>
		public virtual ITemplateFactory TemplateFactory { get; internal set; }
	}
}