using AcspNet.Modules;

namespace AcspNet
{
	/// <summary>
	/// AcspNet controller base class
	/// </summary>
	public class Controller : ModulesAccessor, IController
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
		public IAcspNetContext Context { get; internal set; }

		/// <summary>
		/// Web-site master page data collector.
		/// </summary>
		public virtual IDataCollector DataCollector { get; internal set; }
		
		/// <summary>
		/// Text files reader.
		/// </summary>
		public virtual IFileReader FileReader { get; internal set; }
		
		/// <summary>
		/// Invokes the controller.
		/// </summary>
		public virtual IControllerResponse Invoke()
		{
			return null;
		}
	}
}