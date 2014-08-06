using AcspNet.Modules;

namespace AcspNet
{
	/// <summary>
	/// AcspNet controller base class
	/// </summary>
	public class Controller : ViewAccessor, IController
	{
		/// <summary>
		/// Current AcspNet context
		/// </summary>
		public IAcspNetContext Context { get; internal set; }

		/// <summary>
		/// Current request environment data.
		/// </summary>
		public virtual IEnvironment Environment { get; internal set; }

		/// <summary>
		/// Web-site master page data collector.
		/// </summary>
		public virtual IDataCollector DataCollector { get; internal set; }

		/// <summary>
		/// Text templates loader.
		/// </summary>
		public virtual ITemplateFactory TemplateFactory { get; internal set; }

		/// <summary>
		/// Invokes the controller.
		/// </summary>
		public virtual IControllerResponse Invoke()
		{
			return null;
		}
	}
}