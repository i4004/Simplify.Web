using AcspNet.Modules;
using Simplify.DI;

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
		/// <param name="containerProvider">The DI container provider.</param>
		/// <returns></returns>
		public string Build(IDIContainerProvider containerProvider)
		{
			throw new System.NotImplementedException();
		}
	}
}