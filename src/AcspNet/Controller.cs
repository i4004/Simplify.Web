using AcspNet.Modules;

namespace AcspNet
{
	/// <summary>
	/// AcspNet controller base class
	/// </summary>
	public abstract class Controller : ViewAccessor
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
		/// Web-site master page data collector.
		/// </summary>
		public virtual IDataCollector DataCollector { get; internal set; }
		
		/// <summary>
		/// Text files reader.
		/// </summary>
		public virtual IFileReader FileReader { get; internal set; }

		/// <summary>
		/// Gets the language manager.
		/// </summary>
		/// <value>
		/// The language manager.
		/// </value>
		public virtual ILanguageManager LanguageManager { get; internal set; }

		//public virtual 
		
		/// <summary>
		/// Invokes the controller.
		/// </summary>
		public virtual ControllerResponse Invoke()
		{
			return null;
		}
	}
}