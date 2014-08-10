using AcspNet.Modules;

namespace AcspNet
{
	/// <summary>
	/// Represents controller interface
	/// </summary>
	public interface IController : IViewAccessor
	{
		/// <summary>
		/// Current AcspNet context
		/// </summary>
		IAcspNetContext Context { get; }

		/// <summary>
		/// Current request environment data.
		/// </summary>
		IEnvironment Environment { get; }

		/// <summary>
		/// Web-site master page data collector.
		/// </summary>
		IDataCollector DataCollector { get; }

		/// <summary>
		/// Text templates reader.
		/// </summary>
		ITemplateFactory TemplateFactory { get; }

		/// <summary>
		/// Text files reader.
		/// </summary>
		IFileReader FileReader { get; }

		/// <summary>
		/// Invokes the controller.
		/// </summary>
		IControllerResponse Invoke();
	}
}