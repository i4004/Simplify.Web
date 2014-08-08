using AcspNet.Modules;

namespace AcspNet
{
	/// <summary>
	/// AcspNet view base class 
	/// </summary>
	public abstract class View : ViewAccessor, IView
	{
		/// <summary>
		/// Text templates loader.
		/// </summary>
		public virtual ITemplateFactory TemplateFactory { get; internal set; }
	}
}