using AcspNet.Modules;

namespace AcspNet
{
	/// <summary>
	/// Represent view
	/// </summary>
	public interface IView : IViewAccessor
	{
		/// <summary>
		/// Text templates loader.
		/// </summary>
		ITemplateFactory TemplateFactory { get; }
	}
}