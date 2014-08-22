using AcspNet.Modules;

namespace AcspNet
{
	/// <summary>
	/// Provides controllers responses base class
	/// </summary>
	public abstract class ControllerResponse : ModulesAccessor
	{
		/// <summary>
		/// Gets the data collector.
		/// </summary>
		/// <value>
		/// The data collector.
		/// </value>
		public virtual IDataCollector DataCollector { get; internal set; }

		/// <summary>
		/// Gets the redirector.
		/// </summary>
		/// <value>
		/// The redirector.
		/// </value>
		public virtual IRedirector Redirector { get; internal set; }

		/// <summary>
		/// Processes this response
		/// </summary>
		public abstract ControllerResponseResult Process();
	}
}