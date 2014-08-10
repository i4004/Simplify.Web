using AcspNet.Modules;

namespace AcspNet
{
	/// <summary>
	/// Represent modules accessor
	/// </summary>
	public interface IModulesAccessor : IViewAccessor
	{
		/// <summary>
		/// Gets the current language.
		/// </summary>
		/// <value>
		/// The current language.
		/// </value>
		string Language { get; }

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