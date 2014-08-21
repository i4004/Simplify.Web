using System;
using Simplify.Templates;

namespace AcspNet.Responses
{
	/// <summary>
	/// Provides tempate response (puts data to DataCollector specified variable)
	/// </summary>
	public class InlineTpl : ControllerResponse
	{
		private readonly string _dataCollectorVariableName;
		private readonly string _data;

		/// <summary>
		/// Initializes a new instance of the <see cref="InlineTpl"/> class.
		/// </summary>
		/// <param name="dataCollectorVariableName">Name of the data collector variable.</param>
		/// <param name="template">The template.</param>
		public InlineTpl(string dataCollectorVariableName, ITemplate template)
		{
			if (string.IsNullOrEmpty(dataCollectorVariableName))
				throw new ArgumentNullException("dataCollectorVariableName");

			_dataCollectorVariableName = dataCollectorVariableName;

			if (template != null)
				_data = template.Get();
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="InlineTpl"/> class.
		/// </summary>
		/// <param name="dataCollectorVariableName">Name of the data collector variable.</param>
		/// <param name="data">The data.</param>
		public InlineTpl(string dataCollectorVariableName, string data)
		{
			if (string.IsNullOrEmpty(dataCollectorVariableName))
				throw new ArgumentNullException("dataCollectorVariableName");

			_dataCollectorVariableName = dataCollectorVariableName;
			_data = data;
		}

		/// <summary>
		/// Processes this response
		/// </summary>
		public override ControllerResponseResult Process()
		{
			DataCollector.Add(_dataCollectorVariableName, _data);

			return ControllerResponseResult.Default;
		}
	}
}