using AcspNet.Modules;

namespace AcspNet
{
	/// <summary>
	/// AcspNet controller base class
	/// </summary>
	public class Controller : IController
	{
		/// <summary>
		/// Web-site master page data collector.
		/// </summary>
		public virtual IDataCollector DataCollector { get; internal set; }

		/// <summary>
		/// Invokes the controller.
		/// </summary>
		public virtual IControllerResponse Invoke()
		{
			return null;
		}
	}
}