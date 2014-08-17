using AcspNet.Modules;
using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Provides web-page builder
	/// </summary>
	public class PageBuilder : IPageBuilder
	{
		private readonly string _masterTemplateFileName;
		private readonly ITemplateFactory _templateFactory;
		private readonly IDataCollector _dataCollector;

		/// <summary>
		/// Initializes a new instance of the <see cref="PageBuilder" /> class.
		/// </summary>
		/// <param name="masterTemplateFileName">Name of the master template file.</param>
		/// <param name="templateFactory">The template factory.</param>
		/// <param name="dataCollector">The data collector.</param>
		public PageBuilder(string masterTemplateFileName, ITemplateFactory templateFactory, IDataCollector dataCollector)
		{
			_masterTemplateFileName = masterTemplateFileName;
			_templateFactory = templateFactory;
			_dataCollector = dataCollector;
		}

		/// <summary>
		/// Buids a web page
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <returns></returns>
		public string Build(IDIContainerProvider containerProvider)
		{
			containerProvider.Resolve<IContextVariablesSetter>().SetVariables(containerProvider);

			var tpl = _templateFactory.Load(_masterTemplateFileName);

			foreach (var item in _dataCollector.Items.Keys)
				tpl.Set(item, _dataCollector.Items[item]);

			return tpl.Get();
		}
	}
}