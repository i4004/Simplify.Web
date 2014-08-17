using AcspNet.Modules;

namespace AcspNet.Core
{
	/// <summary>
	/// Provides context variables setter
	/// </summary>
	public class ContextVariablesSetter : IContextVariablesSetter
	{
		private readonly IDataCollector _dataCollector;

		public ContextVariablesSetter(IDataCollector dataCollector)
		{
			_dataCollector = dataCollector;
		}

		/// <summary>
		/// Sets the context variables to data collector
		/// </summary>
		public void SetVariables()
		{
			throw new System.NotImplementedException();
		}
	}
}