using AcspNet.Modules;

namespace AcspNet
{
	/// <summary>
	/// Represent modules accessor
	/// </summary>
	public interface IModulesAccessor : IViewAccessor
	{
		/// <summary>
		/// Current request environment data.
		/// </summary>
		IEnvironment Environment { get; }

		/// <summary>
		/// Text templates factory.
		/// </summary>
		ITemplateFactory TemplateFactory { get; } 
	}
}