using AcspNet.Modules;

namespace AcspNet.Core
{
	/// <summary>
	/// Provides web-page builder
	/// </summary>
	public class PageBuilder : IPageBuilder
	{
		private readonly IDataCollector _dataCollector;

		/// <summary>
		/// Initializes a new instance of the <see cref="PageBuilder"/> class.
		/// </summary>
		/// <param name="dataCollector">The data collector.</param>
		public PageBuilder(IDataCollector dataCollector)
		{
			_dataCollector = dataCollector;
		}

		/// <summary>
		/// Buids a web page
		/// </summary>
		/// <returns></returns>
		public string Buid()
		{
			throw new System.NotImplementedException();
		}
	}
}