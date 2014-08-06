using AcspNet.Modules;

namespace AcspNet
{
	/// <summary>
	/// Represent view
	/// </summary>
	public interface IView : IViewAccessor
	{
		/// <summary>
		/// Text templates reader.
		/// </summary>
		ITemplateFactory TemplateFactory { get; }
	}
}