using AcspNet.Modules;

namespace AcspNet
{
	/// <summary>
	/// Provides controllers responses base class
	/// </summary>
	public abstract class ControllerResponse : IControllerResponse
	{
		/// <summary>
		/// Gets the data collector.
		/// </summary>
		/// <value>
		/// The data collector.
		/// </value>
		public virtual IDataCollector DataCollector { get; internal set; }

		/// <summary>
		/// Processes this response
		/// </summary>
		/// <exception cref="System.NotImplementedException"></exception>
		public virtual void Process()
		{
			throw new System.NotImplementedException();
		}
	}
}