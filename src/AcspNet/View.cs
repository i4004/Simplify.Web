using AcspNet.Modules;

namespace AcspNet
{
	/// <summary>
	/// AcspNet view base class 
	/// </summary>
	public abstract class View : ViewAccessor, IView
	{
		/// <summary>
		/// Gets the current language.
		/// </summary>
		/// <value>
		/// The current language.
		/// </value>
		public virtual string Language { get; internal set; }

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