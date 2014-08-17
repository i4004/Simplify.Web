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
		/// Processes this response
		/// </summary>
		/// <exception cref="System.NotImplementedException"></exception>
		public virtual ControllerResponseResult Process()
		{
			throw new System.NotImplementedException();
		}
	}
}