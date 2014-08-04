using AcspNet.Modules;

namespace AcspNet
{
	/// <summary>
	/// Represents controller interface
	/// </summary>
	public interface IController : IHideObjectMembers
	{
		/// <summary>
		/// Current request environment data.
		/// </summary>
		IEnvironment Environment { get; }

		/// <summary>
		/// Web-site master page data collector.
		/// </summary>
		IDataCollector DataCollector { get; }

		/// <summary>
		/// Invokes the controller.
		/// </summary>
		IControllerResponse Invoke();
	}
}