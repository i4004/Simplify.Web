using AcspNet.Modules;

namespace AcspNet
{
	/// <summary>
	/// Represent view
	/// </summary>
	public interface IView : IViewAccessor
	{
		/// <summary>
		/// Current request environment data.
		/// </summary>
		IEnvironment Environment { get; }

		/// <summary>
		/// Text templates reader.
		/// </summary>
		ITemplateFactory TemplateFactory { get; }
	}
}