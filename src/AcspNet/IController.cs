using AcspNet.Modules;

namespace AcspNet
{
	/// <summary>
	/// Represents controller interface
	/// </summary>
	public interface IController : IModulesAccessor
	{
		/// <summary>
		/// Gets the route parameters.
		/// </summary>
		/// <value>
		/// The route parameters.
		/// </value>
		dynamic RouteParameters { get; }

		/// <summary>
		/// Current AcspNet context
		/// </summary>
		IAcspNetContext Context { get; }

		/// <summary>
		/// Web-site master page data collector.
		/// </summary>
		IDataCollector DataCollector { get; }

		/// <summary>
		/// Text files reader.
		/// </summary>
		IFileReader FileReader { get; }

		/// <summary>
		/// Gets the language manager.
		/// </summary>
		/// <value>
		/// The language manager.
		/// </value>
		ILanguageManager LanguageManager { get; }

		/// <summary>
		/// Invokes the controller.
		/// </summary>
		IControllerResponse Invoke();
	}
}