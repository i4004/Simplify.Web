using AcspNet.Modules;
using AcspNet.Modules.Html;

namespace AcspNet
{
	/// <summary>
	/// AcspNet controllers base class
	/// </summary>
	public abstract class ControllerBase : ViewAccessor
	{
		/// <summary>
		/// Gets the route parameters.
		/// </summary>
		/// <value>
		/// The route parameters.
		/// </value>
		public virtual dynamic RouteParameters { get; internal set; }

		/// <summary>
		/// Current AcspNet context
		/// </summary>
		public virtual IAcspNetContext Context { get; internal set; }

		/// <summary>
		/// Gets the language manager.
		/// </summary>
		/// <value>
		/// The language manager.
		/// </value>
		public virtual ILanguageManager LanguageManager { get; internal set; }

		/// <summary>
		/// Web-site master page data collector.
		/// </summary>
		public virtual IDataCollector DataCollector { get; internal set; }

		/// <summary>
		/// Text files reader.
		/// </summary>
		public virtual IFileReader FileReader { get; internal set; }

		/// <summary>
		/// Gets the redirector.
		/// </summary>
		/// <value>
		/// The redirector.
		/// </value>
		public virtual IRedirector Redirector { get; internal set; }

		/// <summary>
		/// Various HTML generation classes container
		/// </summary>
		/// <value>
		/// The various HTML generation classes container
		/// </value>
		public virtual IHtmlWrapper Html { get; internal set; }
	}
}